﻿namespace TrackerModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public int UnsettledPoints { get; set; }
        public int SettledPenalties { get; set; }
    }
}
