Imports System.Linq.Expressions

Namespace Internal

  Public Class SqlExpressionEntitiesVisitor
    Inherits ExpressionVisitor

    Private m_UsedEntityIndexes As HashSet(Of Int32)

    Public Sub New()

    End Sub

    Public Function GetIndexesOfReferencedEntities(expression As Expression) As Int32()
      m_UsedEntityIndexes = New HashSet(Of Int32)

      Visit(expression)

      Return m_UsedEntityIndexes.OrderBy(Function(x) x).ToArray()
    End Function

    Protected Overrides Function VisitMember(node As MemberExpression) As Expression
      If node.Expression IsNot Nothing AndAlso node.Expression.NodeType = ExpressionType.MemberAccess Then
        Dim node2 = DirectCast(node.Expression, MemberExpression)

        If node2.Expression IsNot Nothing AndAlso node2.Expression.NodeType = ExpressionType.Parameter Then
          If GetType(IJoin).IsAssignableFrom(DirectCast(node2.Expression, ParameterExpression).Type) Then
            m_UsedEntityIndexes.Add(Helpers.Common.GetEntityIndexFromJoinMemberName(node2.Member.Name))
            Return node
          End If
        End If
      End If

      Return MyBase.VisitMember(node)
    End Function

  End Class
End Namespace