//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

public enum PoolLoadTypes
{
    /// <summary>
    /// References to prefabs for use in the pool. Useful in most cases with few prefabs.
    /// </summary>
    Prefabs,
    /// <summary>
    /// Paths to folders with prefabs (has to be located in the project 'Resource' folder). 
    /// Useful in case with a large amount of different prefabs.
    /// </summary>
    Folders
}