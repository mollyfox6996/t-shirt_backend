﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DTOs
{
    public class OperationResultDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}