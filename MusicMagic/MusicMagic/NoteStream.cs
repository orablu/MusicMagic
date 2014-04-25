using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.Storage;
using System.IO;

namespace MusicMagic {
    class NoteStream : INoteStream {
        private Dictionary<int, SortedSet<INote>> notesInPitch;
        private IAsyncAction playTask;
        private String fileLocation;
        public NoteStream() {
            notesInPitch = new Dictionary<int, SortedSet<INote>>();
        }

        public NoteType Type { get; set; }

        public IEnumerable<INote> Notes {
            get {
                var notes = new SortedSet<INote>();
                foreach (var pitch in notesInPitch) {
                    notes.UnionWith(pitch.Value);
                }
                return notes;
            }
            set {
                notesInPitch = new Dictionary<int, SortedSet<INote>>();
                foreach (var note in value) {
                    Add(note);
                }
            }
        }

        public IList<INoteSource> Sources { get; set; }

        public int EarliestTime {
            get {
                return (from note in Notes select note.Start).Min();
            }
        }

        public int LatestTime {
            get {
                return (from note in Notes select note.Start + note.Length).Max();
            }
        }

        public bool Playing {
            get {
                return playTask != null;
            }
        }

        public INoteSource GetSource(int pitch) {
            return Sources[pitch];
        }

        public IEnumerable<INote> NotesInRange(int start, int end) {
            var notes = new HashSet<INote>();
            foreach (var pitch in notesInPitch) {
                notes.UnionWith(from note in pitch.Value
                                where note.Start >= start &&
                                    note.Start + note.Length < end
                                select note);
            }
            return notes;
        }

        public bool UpdateNote(INote note) {
            // Have to search for note since the pitch may have changes, and cannot be properly indexed.
            foreach (var pitch in notesInPitch) {
                pitch.Value.Remove(note);
            }
            var result = Add(note);
            return result;
        }

        public IEnumerator<INote> GetEnumerator() {
            return Notes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Notes.GetEnumerator();
        }

        private bool Add(INote note) {
            if (!notesInPitch.ContainsKey(note.Pitch)) {
                notesInPitch[note.Pitch] = new SortedSet<INote>();
            }
            return notesInPitch[note.Pitch].Add(note);
        }

        public IEnumerable<INote> NotesInRange(TimeSpan start, TimeSpan end) {
            return NotesInRange((int)start.TotalMilliseconds, (int)end.TotalMilliseconds);
        }

        public void Play() {
            Play(EarliestTime, LatestTime + 1);
        }

        public void Play(int start, int end) {
            // Play the source in a non-blocking thread.
            var notes = NotesInRange(start, end);
            playTask = ThreadPool.RunAsync(
                delegate { playAsync(notes); },
                WorkItemPriority.High);
            playTask.Completed = (action, status) => {
                foreach (var source in Sources) {
                    source.Voice.Stop();
                }
            };
        }

        public void PlayPitch(int pitch) {
            Sources[pitch].Play();
        }

        public void Stop() {
            playTask.Cancel();
            playTask = null;
        }

        private void playAsync(IEnumerable<INote> notes) {
            var iterator = notes.GetEnumerator();

            // Play the first note.
            var current = iterator.Current;
            bool hasNext = iterator.MoveNext();
            var next = iterator.Current;
            if (current != null) {
                current.Play();
            }

            // Play the rest of the notes.
            while (hasNext) {
                // Wait for the next note.
                var ms = next.Start - current.Start;
                if (ms > 0) {
                    new System.Threading.ManualResetEvent(false).WaitOne(ms);
                }

                // Move to the next node and play.
                current = next;
                hasNext = iterator.MoveNext();
                next = iterator.Current;
                current.Play();
            }
        }

        public void serialize()
        {

        }
        //CSV format: piano, startTime, LengthOfNote, Pitch
        public void loadStream()
        {
            var localStorage = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = localStorage.GetFileAsync(fileLocation).GetResults();
            var folder = ApplicationData.Current.LocalFolder;
            switch (this.Type)
            {
                case NoteType.Drum:
                    file = folder.CreateFileAsync("drum.csv").GetResults();
                    break;
                case NoteType.Guitar:
                    file = folder.CreateFileAsync("guitar.csv").GetResults();
                    break;
                case NoteType.Piano:
                    file = folder.CreateFileAsync("piano.csv").GetResults();
                    break;
            }
            var lines = FileIO.ReadLinesAsync(file).GetResults();
            foreach (string line in lines)
            {
                var words = line.Split(',');
                    var note = new Note();
                    note.Start = Convert.ToInt32(words[0]);
                    note.Length = Convert.ToInt32(words[1]);
                    note.Pitch = Convert.ToInt32(words[2]);
                    Add(note);
                
            }
        }
        //CSV format: piano, startTime, LengthOfNote, Pitch
        public void saveStream()
        {
            string [] csv = new string[4];
            var folder = ApplicationData.Current.LocalFolder;
            var file = folder.CreateFileAsync(fileLocation).GetResults();
            foreach (KeyValuePair<int, SortedSet<INote>> allNotes in notesInPitch)
            {
                foreach (Note note in allNotes.Value) {
                    csv[0] = Convert.ToString(note.Start) + ", ";
                    csv[1] = Convert.ToString(note.Length) + ", ";
                    csv[2] = Convert.ToString(note.Pitch);
                    csv[3] = Environment.NewLine;
                    
                }
                switch(this.Type){
                    case NoteType.Drum:
                        file = folder.CreateFileAsync("drum.csv").GetResults();
                        break;
                    case NoteType.Guitar:
                        file = folder.CreateFileAsync("guitar.csv").GetResults();
                        break;
                    case NoteType.Piano:
                        file = folder.CreateFileAsync("piano.csv").GetResults();
                        break;
                }
                FileIO.WriteLinesAsync(file, csv);
            }
        }
    }
}
