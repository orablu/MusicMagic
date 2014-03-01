using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMagic {
    interface INoteStream : IEnumerable<INote> {
        IEnumerable<INote> Notes { get; set; }
        IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end);
        IEnumerator<INote> GetEnumerator();
    }
}
