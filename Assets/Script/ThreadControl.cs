using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ThreadControl : MonoBehaviour {
        public static ThreadControl Main;
        public List<Event> Thread;
        public Event CurrentEvent;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            EventUpdate();
        }

        public void AddEvent(Event E)
        {
            Thread.Add(E);
        }

        public void RemoveEvent(int Index)
        {
            if (Index >= 0 && Index < Thread.Count)
                Thread.RemoveAt(Index);
        }

        public void StartProcess()
        {
            if (!CanStartProcess())
                return;
            NextEventProcess();
        }

        public bool CanStartProcess()
        {
            if (Thread.Count <= 0)
                return false;
            return true;
        }

        public void EventUpdate()
        {
            if (GetCurrentEvent())
                GetCurrentEvent().EffectUpdate(Time.deltaTime);
        }

        public void NextEventProcess()
        {
            if (Thread.Count > 0)
            {
                Event E = Thread[0];
                Thread.RemoveAt(0);
                StartEvent(E);
            }
        }

        public void StartEvent(Event E)
        {
            if (!E)
                return;
            CurrentEvent = E;
            E.Effect();
        }

        public void EndEvent()
        {
            if (!GetCurrentEvent())
                return;
            GetCurrentEvent().OnEnd();
            CurrentEvent = null;
            NextEventProcess();
        }

        public Event GetCurrentEvent()
        {
            return CurrentEvent;
        }

        public Event GetEvent(int Index)
        {
            if (Index < 0 || Index >= Thread.Count)
                return null;
            return Thread[Index];
        }
    }
}