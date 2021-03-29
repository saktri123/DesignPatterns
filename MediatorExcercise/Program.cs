using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding.Exercise
{
    public class Participant
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public Mediator Mediator { get; set; }

        public Participant(Mediator mediator)
        {
            Mediator = mediator;
            mediator.participants.Add(this);
        }

        public void Say(int n)
        {
            Mediator.BroadCast(this.Name, n);
        }

        public void Received(int n) {
            Value += n;
        }
    }

    public class Mediator
    {
       public List<Participant> participants = new List<Participant>();

        public void BroadCast(string source, int value) {
            foreach (var participant in participants)
            {
                if (participant.Name != source)
                    participant.Received(value);
            }
        }

        void SnapShot() {
            StringBuilder sb = new StringBuilder();
            foreach (var item in participants)
            {
                sb.Append($"{item.Name}: {item.Value} ;");
            }
            Console.WriteLine(sb.ToString());
        }

        public static void Main() {
            Mediator mediator = new Mediator();
            var p1 = new Participant(mediator) { Name="P1"};
            var p2 = new Participant(mediator) { Name = "P2" };

            mediator.SnapShot();

            p1.Say(2);

            mediator.SnapShot();

            p2.Say(4);

            mediator.SnapShot();

        }
    }

}
