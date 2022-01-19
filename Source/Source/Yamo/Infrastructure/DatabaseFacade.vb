Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics.CodeAnalysis

Namespace Infrastructure

  ''' <summary>
  ''' Provides access to database connection and transaction.
  ''' </summary>
  Public Class DatabaseFacade
    Implements IDisposable

    ''' <summary>
    ''' Stores associated context.
    ''' </summary>
    Private m_Context As DbContext

    ''' <summary>
    ''' Stores info whether associated context is database connection owner or not.
    ''' </summary>
    Private m_IsConnectionOwner As Boolean

    Private m_Connection As DbConnection
    ''' <summary>
    ''' Gets database connection.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Connection As DbConnection
      Get
        Return m_Connection
      End Get
    End Property

    Private m_Transaction As DbTransaction
    ''' <summary>
    ''' Gets database transaction.
    ''' </summary>
    ''' <returns>Transaction or <see langword="Nothing"/> if transaction is not started.</returns>
    Public ReadOnly Property Transaction As <MaybeNull> DbTransaction
      Get
        Return m_Transaction
      End Get
    End Property

    ''' <summary>
    ''' Creates new instance of <see cref="DatabaseFacade"/>.<br/>
    ''' This API supports Yamo infrastructure and is not intended to be used directly from your code.
    ''' </summary>
    ''' <param name="context"></param>
    Sub New(<DisallowNull> context As DbContext)
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

    ''' <summary>
    ''' Begins transaction.
    ''' </summary>
    ''' <param name="isolationLevel"></param>
    ''' <returns></returns>
    Public Function BeginTransaction(Optional isolationLevel As IsolationLevel? = Nothing) As DbTransaction
      If Me.Transaction IsNot Nothing Then
        Throw New InvalidOperationException("Cannot begin another transaction.")
      End If

      m_Transaction = Me.Connection.BeginTransaction(isolationLevel.GetValueOrDefault(Data.IsolationLevel.Serializable))

      Return m_Transaction
    End Function

    ''' <summary>
    ''' Commits transaction.
    ''' </summary>
    Public Sub CommitTransaction()
      If Me.Transaction Is Nothing Then
        Throw New InvalidOperationException("Transaction not started.")
      End If

      Me.Transaction.Commit()
      m_Transaction = Nothing
    End Sub

    ''' <summary>
    ''' Rollbacks transaction.
    ''' </summary>
    Public Sub RollbackTransaction()
      If Me.Transaction Is Nothing Then
        Throw New InvalidOperationException("Transaction not started.")
      End If

      Me.Transaction.Rollback()
      m_Transaction = Nothing
    End Sub

#Region "IDisposable Support"
    Private m_DisposedValue As Boolean

    ''' <summary>
    ''' Releases all resources used by the <see cref="DatabaseFacade"/> class.
    ''' </summary>
    ''' <param name="disposing"></param>
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

    ''' <summary>
    ''' Releases all resources used by the <see cref="DatabaseFacade"/> class.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
      Dispose(True)
    End Sub
#End Region

  End Class
End Namespace