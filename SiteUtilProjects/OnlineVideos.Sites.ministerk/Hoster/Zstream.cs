﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OnlineVideos.Hoster
{
    public class Zstream : HosterBase, ISubtitle
    {
        public override string GetHosterUrl()
        {
            return "zstream.to";
        }

        public override string GetVideoUrl(string url)
        {

            string data = GetWebData<string>(url);
            sub = "";
            Regex rgx = new Regex(@"""(?<u>http[^""]*?.mp4[^""]*)""");
            Match m = rgx.Match(data);
            if (m.Success)
            {
                SetSub(data);
                return m.Groups["u"].Value;
            }
            return "";
        }

        private string sub = "";
        private void SetSub(string data)
        {
            try
            {
                Regex r = new Regex(@"""(?<u>http[^""]*?.srt[^""]*)""");
                Match m = r.Match(data);
                if (m.Success)
                    sub = GetWebData(m.Groups["u"].Value, encoding: Encoding.Default);
                else
                    sub = "";
            }
            catch
            {
                sub = "";
            }
        }

        public string SubtitleText
        {
            get { return sub; }
        }
    }
}
