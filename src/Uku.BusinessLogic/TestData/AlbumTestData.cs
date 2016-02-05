using Uku.BusinessLogic.Service;
using Uku.Database.Model;

namespace Uku.BusinessLogic.TestData
{
    public class AlbumTestData
    {
        IAlbumService albumService;

        public AlbumTestData(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public void CreateAlbums()
        {
            CreateAlbum("Pet Sounds");
            CreateAlbum("Revolver");
            CreateAlbum("Nevermind");
            CreateAlbum("The Velvet Underground & Nico");
            CreateAlbum("Sgt. Pepper's Lonely Hearts Club Band");
            CreateAlbum("What's Going On");
            CreateAlbum("Blonde on Blonde");
            CreateAlbum("Exile on Main St.");
            CreateAlbum("London Calling");
            CreateAlbum("Never Mind the Bollocks Here's the Sex Pistols");
            CreateAlbum("Highway 61 Revisited");
            CreateAlbum("Are You Experienced?");
            CreateAlbum("OK Computer");
            CreateAlbum("The Beatles ('White Album')");
            CreateAlbum("Astral Weeks");
            CreateAlbum("The Rise and Fall of Ziggy Stardust and the Spiders from Mars");
            CreateAlbum("It Takes a Nation of Millions to Hold Us Back");
            CreateAlbum("Born to Run");
            CreateAlbum("Horses");
            CreateAlbum("Abbey Road");
            CreateAlbum("The Dark Side of the Moon");
            CreateAlbum("Electric Ladyland");
            CreateAlbum("Blood on the Tracks");
            CreateAlbum("The Doors");
            CreateAlbum("Marquee Moon");
            CreateAlbum("Sign 'O' the Times");
            CreateAlbum("Thriller");
            CreateAlbum("The Queen Is Dead");
            CreateAlbum("Rubber Soul");
            CreateAlbum("Beggars Banquet");
            CreateAlbum("Led Zeppelin IV");
            CreateAlbum("Blue Lines");
            CreateAlbum("Who's Next");
            CreateAlbum("Funeral");
            CreateAlbum("Remain in Light");
            CreateAlbum("Let It Bleed");
            CreateAlbum("Ramones");
            CreateAlbum("The Joshua Tree");
            CreateAlbum("Kind of Blue");
            CreateAlbum("'Live' at the Apollo");
            CreateAlbum("Closer");
            CreateAlbum("Innervisions");
            CreateAlbum("Sticky Fingers");
            CreateAlbum("After the Gold Rush");
            CreateAlbum("Is This It");
            CreateAlbum("Songs in the Key of Life");
            CreateAlbum("The Band");
            CreateAlbum("Blue");
            CreateAlbum("Forever Changes");
            CreateAlbum("Purple Rain");
            CreateAlbum("There's a Riot Goin' On");
            CreateAlbum("Kid A");
            CreateAlbum("Automatic for the People");
            CreateAlbum("Trout Mask Replica");
            CreateAlbum("The Stone Roses");
            CreateAlbum("The Clash");
            CreateAlbum("Odelay");
            CreateAlbum("Rumours");
            CreateAlbum("Doolittle");
            CreateAlbum("Appetite for Destruction");
            CreateAlbum("Otis Blue/Otis Redding Sings Soul");
            CreateAlbum("Hunky Dory");
            CreateAlbum("Led Zeppelin II");
            CreateAlbum("Unknown Pleasures");
            CreateAlbum("Murmur");
            CreateAlbum("Tapestry");
            CreateAlbum("A Love Supreme");
            CreateAlbum("John Lennon/Plastic Ono Band");
            CreateAlbum("Grace");
            CreateAlbum("Dummy");
            CreateAlbum("Yankee Hotel Foxtrot");
            CreateAlbum("Daydream Nation");
            CreateAlbum("Bringing It All Back Home");
            CreateAlbum("Loveless");
            CreateAlbum("Layla and Other Assorted Love Songs");
            CreateAlbum("Graceland");
            CreateAlbum("3 Feet High and Rising");
            CreateAlbum("Transformer");
            CreateAlbum("This Year's Model");
            CreateAlbum("Music from Big Pink");
            CreateAlbum("I Never Loved a Man the Way I Love You");
            CreateAlbum("Achtung Baby");
            CreateAlbum("Fun House");
            CreateAlbum("Psychocandy");
            CreateAlbum("Screamadelica");
            CreateAlbum("The Bends");
            CreateAlbum("(What's the Story) Morning Glory?");
            CreateAlbum("Bitches Brew");
            CreateAlbum("Low");
            CreateAlbum("Raw Power");
            CreateAlbum("Endtroducing.....");
            CreateAlbum("Trans-Europa Express");
            CreateAlbum("Imagine");
            CreateAlbum("Moondance");
            CreateAlbum("Back in Black");
            CreateAlbum("Surfer Rosa");
            CreateAlbum("Harvest");
            CreateAlbum("Physical Graffiti");
            CreateAlbum("The Piper at the Gates of Dawn");
            CreateAlbum("Swordfishtrombones");
            albumService.SaveChanges();
        }

        private void CreateAlbum(string title)
        {
            albumService.Create(new Album { Title = title }, false);
        }
    }
}
