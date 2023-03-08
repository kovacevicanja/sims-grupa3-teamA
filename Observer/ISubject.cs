using System;
using System.Collections.Generic;
using System.Text;

namespace OisisiProjekat.Observer
{
    interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void NotifyObservers();
    }
}
