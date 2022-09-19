//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

using System;

public interface IPoolGlobalEvent
{
    /// <summary>
    /// Invoke object pool's global event.
    /// </summary>
    /// <param name="sender">Reference to sender (Game Object on the Scene).</param>
    /// <param name="data">Any custom data. For 'OnObtain' and 'OnRelease' events it will be a reference to Game Object.</param>
    void Invoke(object sender, object data);
    /// <summary>
    /// Add new handler to object pool's global event.
    /// </summary>
    /// <param name="handler">Event handler (for example, method with two arguments: object sender, object data).</param>
    void Subscribe(EventHandler<object> handler);
    /// <summary>
    /// Remove handler from object pool's global event.
    /// </summary>
    /// <param name="handler">Event handler (for example, method with two arguments: object sender, object data).</param>
    void Unsubscribe(EventHandler<object> handler);
}