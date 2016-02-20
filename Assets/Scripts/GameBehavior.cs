
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using System.Timers;

namespace Modeler
{
    public class GameBehavior : MonoBehaviour
    {
        protected void doInBackground(System.ComponentModel.DoWorkEventHandler main,
                             System.ComponentModel.RunWorkerCompletedEventHandler completed)
        {
            System.ComponentModel.BackgroundWorker worker =
            new System.ComponentModel.BackgroundWorker();

            worker.DoWork += main;
            worker.RunWorkerCompleted += completed;
            worker.RunWorkerAsync();
        }

        protected void ShootRay(Action<RaycastHit> action)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                action(hitInfo);
            }
        }

        protected string Now()
        {
            return DateTime.Now.ToString("MM_dd_yyyy.HH_mm_ss");
        }

        protected string ToJSON(GameObject obj)
        {
            return JsonUtility.ToJson(obj);
        }

        protected T FromJSON<T>(string json_)
        {
            return JsonUtility.FromJson<T>(json_);
        }


        private static int uiFocus = 0;
        protected void UIisFocused()
        {
            uiFocus += 1;
        }

        protected void UIisUnfocused()
        {
            uiFocus -= 1;
        }

        protected bool isUIFocused()
        {
            return uiFocus > 0;
        }

        public void EventListener(GameObject obj, EventTriggerType eventType, Action handler)
        {
            if (obj.GetComponent<EventTrigger>() == null)
            {
                //add the event trigger
                obj.AddComponent<EventTrigger>();
            }

            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = eventType;
            entry.callback.AddListener((eventData) => {
                handler();
            });

            if (trigger.triggers == null)
            {
                trigger.triggers = new List<EventTrigger.Entry>();
            }
            trigger.triggers.Add(entry);

        }

        public void EventListener(EventTriggerType type, Action handler)
        {
            EventListener(this.gameObject, type, handler);
        }

        //NOTE, this callback will be run in a separate thread. Absolutely no Unity API can be used...only .NET classes and C# primitives
        //can be used inside this callback.
        protected void Timer(float timeInSeconds, Action callback)
        {
            Timer t_ = new Timer(timeInSeconds * 1000.0f);
            t_.Elapsed += (o, args) => {
                callback();
            };
            t_.AutoReset = false;
            t_.Start();
        }
    }
}
