﻿if (typeof(module) !== "undefined") {
    module.exports = PathBuilder;
}

function PathNode(name, folder, children) {
    var self = this;

    self.$name = name;
    self.$folder = folder;
    self.$children = children || [];
    self.$children.map(function (node) {
        self[node.$name] = node;
    });
    self.$parent = null;
    self.$fixup = function () {
        self.$children.map(function (node) {
            node.$parent = self;
            node.$fixup();
            node.$parent["$" + node.$name] = node.$$path();
            node.$parent[node.$name + "$"] = node.$folder;
        });
        return self;
    };
    self.$$path = function () {
        if (isArray()) {
            return self.$parent ? self.$folder.map(function (f) { return self.$parent.$$path() + "/" + f; }) : self.$folder;
        }
        else {
            return self.$parent ? self.$parent.$$path() + "/" + self.$folder : self.$folder;
        }
    };
    function isArray() {
        return Object.prototype.toString.call(self.$folder) === '[object Array]';
    }
}

function PathBuilder() {
    this.build = function (data) {
        var root = new PathNode("root", ".", buildThese(data));
        root.$fixup();
        return root;
    };
    function buildThese(nodes) {
        return nodes.map(function (n) {
            var children = (n.length > 2) ? buildThese(n[2]) : null;
            return new PathNode(n[0], n[1], children);
        });
    }
}
