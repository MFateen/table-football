using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct Anticipate {
    public int Row;
    public int Column;
    public bool Near;
    public Anticipate(int _Row, int _Column, bool _Near) {
        Row = _Row;
        Column = _Column;
        Near = _Near;
    }
}
