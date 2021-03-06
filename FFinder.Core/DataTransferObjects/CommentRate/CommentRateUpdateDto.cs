﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FFinder.Core.DataTransferObjects.CommentRate
{
    public class CommentRateUpdateDto
    {
        public string CommentRateId { get; set; }
        public bool IsLike { get; set; }
        public DateTime RateDate { get; set; }
        public string CommentId { get; set; }
        public string OwnerId { get; set; }
        public bool IsActive { get; set; }
    }
}
