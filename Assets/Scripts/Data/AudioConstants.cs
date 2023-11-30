namespace Data
{
    public static class AUDIOEVENT
    {
        public const string Event_1 = "Event_1";
        
        //...
    }

    public static class AUDIOSWITCH
    {
        public const string GROUND = "Ground";
        public const string WATER = "Water";
        
        //...
    }

    public static class AUDIOVOLUME
    {
        public const string MASTER = "Master";
        public const string SFX = "SFX";
        public const string MUSIC = "Music";
        
        //...
    }
    
    public enum STOPMODE
    {
        IMMEDIATELY,
        WAIT_SECOND
        
        //...
    }
}