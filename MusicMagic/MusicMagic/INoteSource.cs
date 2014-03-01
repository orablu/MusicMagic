using SharpDX.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMagic {
    interface INoteSource {
        string Path { get; set; }
        int LoopBegin { get; set; }
        int LoopLength { get; set; }
    }
}
