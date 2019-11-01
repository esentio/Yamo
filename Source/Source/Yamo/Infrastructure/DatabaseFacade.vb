Imports System.Data
Imports System.Data.Common

Namespace Infrastructure

  ' TODO: SIP - add documentation to this class.
  Public Class DatabaseFacade
    Implements IDisposable

    Private m_Context As DbContext

    Private m_IsConnectionOwner As Boolean

    Private m_Connection As DbConnection
    Public ReadOnly Property Connection As DbConnection
      Get
        Return m_Connection
      End Get
    End Property

    Private m_Transaction As DbTransaction
    Public ReadOnly Property Transaction As DbTransaction
      Get
        Return m_Transaction
      End Get
    End Property

    Sub New(context As DbContext)
      m_Context = context
      m_Connection = context.Options.Connection

      If m_Connection Is Nothing Then
        If context.Options.ConnectionFactory Is Nothing Then
          Throw New Exception("Connection not provided.")
        End If

        m_Connection = context.Options.ConnectionFactory.Invoke()
        m_IsConnectionOwner = True
      Else
        m_IsConnectionOwner = False
      End If

      If m_Connection.State = ConnectionState.Closed Then
        m_Connection.Open()
      End If
    End Sub

    Public Function BeginTransaction(Optional isolationLevel As IsolationLevel? = Nothing) As DbTransaction
      If Me.Transaction IsNot Nothing Then
        Throw New InvalidOperationException("Cannot begin another transaction.")
      End If

      m_Transaction = Me.Connection.BeginTransaction(isolationLevel.GetValueOrDefault(Data.IsolationLevel.Serializable))

      Return m_Transaction
    End Function

    Public Sub CommitTransaction()
      If Me.Transaction Is Nothing Then
        Throw New InvalidOperationException("Transaction not started.")
      End If

      Me.Transaction.Commit()
      m_Transaction = Nothing
    End Sub

    Public Sub RollbackTransaction()
      If Me.Transaction Is Nothing Then
        Throw New InvalidOperationException("Transaction not started.")
      End If

      Me.Transaction.Rollback()
      m_Transaction = Nothing
    End Sub

#Region "IDisposable Support"
    Private m_DisposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
      If Not m_DisposedValue Then
        If disposing Then
          If Me.Transaction IsNot Nothing Then
            Me.Transaction.Dispose()
            m_Transaction = Nothing
          End If

          If m_IsConnectionOwner AndAlso Me.Connection IsNot Nothing Then
            Me.Connection.Dispose()
            m_Connection = Nothing
          End If
        End If
      End If
      m_DisposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
      Dispose(True)
    End Sub
#End Region

  End Class
End Namespace