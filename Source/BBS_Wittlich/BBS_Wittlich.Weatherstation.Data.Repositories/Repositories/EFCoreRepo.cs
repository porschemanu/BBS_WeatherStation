﻿using BBS_Wittlich.Weatherstation.Data.Interfaces;
using BBS_Wittlich.Weatherstation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Repositories
{
    public class EFCoreRepo : IRepositories
    {
        BBS_WetterstationContext _context = new BBS_WetterstationContext();
        string IRepositories.Source { get; set; }

        private string _source;

        public WeatherEntry[] Get()
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic, DateTime date)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }
    }
}
