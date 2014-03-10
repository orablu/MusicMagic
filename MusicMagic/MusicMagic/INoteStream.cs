using System;
using System.Collections.Generic;

namespace MusicMagic {
    interface INoteStream : IEnumerable<INote> {
        enum NoteType { Undefined, Piano, Guitar, Drum, Vocal };
        NoteType Type { get; set; }
        IEnumerable<INote> Notes { get; set; }
        IList<INoteSource> Sources { get; set; }
        INoteSource GetSource(int pitch);
        IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end);
        IEnumerator<INote> GetEnumerator();
    }
}
