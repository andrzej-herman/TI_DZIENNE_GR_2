﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data
{
    public class CheckAnswerDto
    {
        public bool IsCorrectAnswer { get; set; }
        public int NextCategory { get; set; }
    }
}
