describe("PathBuilder", function () {

    it("can be created", function () {
        var pb = new PathBuilder();
        expect(pb).toBeDefined();
    });

    it("can be built", function () {
        var pb = new PathBuilder();
        var paths = pb.build(pathdata);
        expect(paths).toBeDefined();
    });

    it("returns a simple path", function () {
        var paths = makePaths([["dist", "wwwroot"]]);
        expect(paths.$dist).toEqual("./wwwroot");
    });

    it("returns a simple name", function () {
        var paths = makePaths([["dist", "wwwroot"]]);
        expect(paths.dist$).toEqual("wwwroot");
    });

    it("returns second level paths", function () {
        var paths = makePaths([["dist", "wwwroot", [["styles", "css"], ["javascript", "js"]]]]);
        expect(paths.dist.$styles).toEqual("./wwwroot/css");
        expect(paths.dist.$javascript).toEqual("./wwwroot/js");
    });

    it("returns second level names", function () {
        var paths = makePaths([["dist", "wwwroot", [["styles", "css"], ["javascript", "js"]]]]);
        expect(paths.dist.styles$).toEqual("css");
        expect(paths.dist.javascript$).toEqual("js");
    });

    it("returns array of paths", function () {
        var paths = makePaths(pathdata);
        expect(paths.less.$dev_src).toEqual(["./Less/main.less", "./Less/album.less"]);
    });

    var pathdata = [
        ["dist", "wwwroot", [
            ["css", "css"],
            ["js", "js"]
        ]],
        ["less", "Less", [
            ["all", "**/*.less"],
            ["dev_src", ["main.less", "album.less"]],
            ["lib", "lib", [
                ["files", "**/*.less"],
                ["bootstrap", "uku-bootstrap.less"],
                ["fontawesome", "uku-fontawesome.less"]
            ]]
        ]]
    ];

    function makePaths(pathdata) {
        var pb = new PathBuilder();
        return pb.build(pathdata);
    }
});