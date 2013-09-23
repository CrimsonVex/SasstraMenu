using UnityEngine;
using System;
using System.Collections;
using System.Linq.Expressions;

public class MemberInfoGetting : MonoBehaviour
{
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}
