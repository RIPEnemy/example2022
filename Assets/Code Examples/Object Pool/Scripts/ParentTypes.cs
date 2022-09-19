//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

public enum ParentTypes
{
    /// <summary>
    /// All new objects will be created without parent. Useful for a large number of moving objects.
    /// </summary>
    None = 0,
    /// <summary>
    /// All new objects will be created as this game object children. Useful for static objects.
    /// </summary>
    Pool = 1,
    /// <summary>
    /// All new objects will be created as children of custom game object (selected below or
    /// passed as argument to 'Obtain' method). Useful in case which you need to manually
    /// control parent for obtained objects.
    /// </summary>
    Custom = 2
}