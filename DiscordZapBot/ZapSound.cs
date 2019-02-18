using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordZapBot
{
    public class ZapSound
    {
        public static ZapSound Hello = new ZapSound("sounds\\hello.mp3");
        public static ZapSound Broken = new ZapSound("sounds\\broken.mp3");
        public static ZapSound GodNo = new ZapSound("sounds\\godno.mp3");
        public static ZapSound Attitude = new ZapSound("sounds\\dontneedatt.mp3");
        public static ZapSound AskQuestion = new ZapSound("sounds\\askques.mp3");
        public static ZapSound Assasinate = new ZapSound("sounds\\assas.mp3");
        public static ZapSound Assasinate2 = new ZapSound("sounds\\assas-2.mp3");
        public static ZapSound Boots = new ZapSound("sounds\\boots.mp3");
        public static ZapSound BrannigansLaw = new ZapSound("sounds\\branlaw.mp3");
        public static ZapSound Broad = new ZapSound("sounds\\broad.mp3");
        public static ZapSound CantAllow = new ZapSound("sounds\\cantallow.mp3");
        public static ZapSound Conaroused = new ZapSound("sounds\\conaroused.mp3");
        public static ZapSound DealFemale = new ZapSound("sounds\\dealfem-2.mp3");
        public static ZapSound DisgustMeMore = new ZapSound("sounds\\disgustmemore.mp3");
        public static ZapSound Dogfight = new ZapSound("sounds\\dogfight-2.mp3");
        public static ZapSound HaveSex = new ZapSound("sounds\\havesex.mp3");
        public static ZapSound ImPathetic = new ZapSound("sounds\\impath.mp3");
        public static ZapSound Lonely = new ZapSound("sounds\\lonely.mp3");
        public static ZapSound MadeABra = new ZapSound("sounds\\madebra.mp3");
        public static ZapSound MadeItWithAWoman = new ZapSound("sounds\\madewithwoman-2.mp3");
        public static ZapSound MakeTimeTogether = new ZapSound("sounds\\maketimetog.mp3");
        public static ZapSound ManDream = new ZapSound("sounds\\mandream.mp3");
        public static ZapSound Menu = new ZapSound("sounds\\menu.mp3");
        public static ZapSound MornLover = new ZapSound("sounds\\mornlover.mp3");
        public static ZapSound MyWoman = new ZapSound("sounds\\mywoman.mp3");
        public static ZapSound Noodle = new ZapSound("sounds\\noodle.mp3");
        public static ZapSound NotGuilty = new ZapSound("sounds\\notguilty.mp3");
        public static ZapSound Order = new ZapSound("sounds\\order.mp3");
        public static ZapSound Rav = new ZapSound("sounds\\rav.mp3");
        public static ZapSound SchoolGirls = new ZapSound("sounds\\schoolgirls.mp3");
        public static ZapSound Seduction = new ZapSound("sounds\\seduction.mp3");
        public static ZapSound Tail = new ZapSound("sounds\\tail.mp3");
        public static ZapSound Underarrest = new ZapSound("sounds\\underarrest.mp3");
        public static ZapSound Work = new ZapSound("sounds\\work.mp3");
        public static ZapSound Work2 = new ZapSound("sounds\\work-2.mp3");


        public static List<ZapSound> Sounds = new List<ZapSound>
        {
            //Hello, // Exclusive for join
            Broken,
            GodNo,
            Attitude,
            AskQuestion,
            Assasinate,
            Assasinate2,
            Boots,
            BrannigansLaw,
            Broad,
            CantAllow,
            Conaroused,
            DealFemale,
            DisgustMeMore,
            Dogfight,
            HaveSex,
            ImPathetic,
            Lonely,
            MadeABra,
            MadeItWithAWoman,
            MakeTimeTogether,
            ManDream,
            Menu,
            MornLover,
            MyWoman,
            Noodle,
            NotGuilty,
            Order,
            Rav,
            SchoolGirls,
            Seduction,
            Tail,
            Underarrest,
            Work,
            Work2
        };

        public static ZapSound RandomSound()
        {
            return Sounds.Random();
        }



        public string Filename { get; set; }

        public ZapSound(string filename)
        {
            Filename = filename;
        }
    }
}
