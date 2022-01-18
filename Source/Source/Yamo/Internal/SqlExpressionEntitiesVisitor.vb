Imports System.Diagnostics.CodeAnalysis
Imports System.Linq.Expressions

Namespace Internal

  ''' <summary>
  ''' SQL expression entities visitor.<br/>
  ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
  ''' </summary>
  Public Class SqlExpressionEntitiesVisitor
    Inherits ExpressionVisitor

    ''' <summary>
    ''' Stores used entity indexes.
    ''' </summary>
    Private m_UsedEntityIndexes As HashSet(Of Int32)

    ''' <summary>
    ''' Creates new instance of <see cref="SqlExpressionEntitiesVisitor"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Get indexes of referenced entities.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns></returns>
    Public Function GetIndexesOfReferencedEntities(<DisallowNull> expression As Expression) As Int32()
      m_UsedEntityIndexes = New HashSet(Of Int32)

      Visit(expression)

      Return m_UsedEntityIndexes.OrderBy(Function(x) x).ToArray()
    End Function

    ''' <summary>
    ''' Visits member.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
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