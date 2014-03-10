using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

namespace MusicMagic {
    interface INote {
        INoteStream Stream { get; set; }
        double Start { get; set; }
        double Length { get; set; }
        int Pitch { get; set; }
        SoundStream Stream { get; }
        void Play();
    }
}
