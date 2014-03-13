using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

namespace MusicMagic {
    interface INote : IComparable<INote> {
        INoteStream Parent { get; set; }
        int Start { get; set; }
        int Length { get; set; }
        int Pitch { get; set; }
        void Play();
    }
}
