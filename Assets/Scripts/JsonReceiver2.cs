using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonReceiver2
{
    public Comment[] comments;
}

[Serializable]
public class Comment
{
    public int postId;
    public int id;
    public string name;
    public string email;
    public string body;
}
