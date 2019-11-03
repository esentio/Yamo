Imports System.Data.Common
Imports Yamo.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal
Imports Yamo.Metadata

''' <summary>
''' A <see cref="DbContext"/> instance represents a session with the database and is used to execute queries against it.
''' </summary>
Public Class DbContext
  Implements IDisposable

  Private m_Model As Model
  ''' <summary>
  ''' Entities, relationships and their mapping to the database.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property Model As Model
    Get
      If m_Model Is Nothing Then
        m_Model = ModelCache.GetModel(Me.Options.DialectProvider, Me)
      End If

      Return m_Model
    End Get
  End Property

  Private m_Options As DbContextOptions
  ''' <summary>
  ''' Configuration options.
  ''' </summary>
  ''' <returns></returns>
  Friend ReadOnly Property Options As DbContextOptions
    Get
      If m_Options Is Nothing Then
        CreateOptions()
      End If

      Return m_Options
    End Get
  End Property

  Private m_Database As DatabaseFacade
  ''' <summary>
  ''' Provides access to database connection and transaction.
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property Database As DatabaseFacade
    Get
      If m_Database Is Nothing Then
        CreateDatabase()
      End If

      Return m_Database
    End Get
  End Property

  ''' <summary>
  ''' Initializes a new instance of the <see cref="DbContext"/> class.
  ''' </summary>
  Sub New()
  End Sub

  ''' <summary>
  ''' Creates model metadata instance.
  ''' </summary>
  ''' <returns></returns>
  Friend Function CreateModel() As Model
    Dim modelBuilder = New ModelBuilder
    OnModelCreating(modelBuilder)
    Return modelBuilder.Model()
  End Function

  ''' <summary>
  ''' Creates configuration options instance.
  ''' </summary>
  Private Sub CreateOptions()
    Dim optionsBuilder = New DbContextOptionsBuilder
    OnConfiguring(optionsBuilder)
    m_Options = optionsBuilder.Options
  End Sub

  ''' <summary>
  ''' Creates database facade instance.
  ''' </summary>
  Private Sub CreateDatabase()
    m_Database = New DatabaseFacade(Me)
  End Sub

  ''' <summary>
  ''' Notifies that DbCommand is going to be executed.
  ''' </summary>
  ''' <param name="command"></param>
  Friend Sub NotifyCommandExecution(command As DbCommand)
    OnCommandExecuting(command)
  End Sub

  ''' <summary>
  ''' Override this method to configure the database for this context.<br/>
  ''' This method is called once for each instance of the context that is created.<br/>
  ''' The base implementation does nothing.
  ''' </summary>
  ''' <param name="optionsBuilder"></param>
  Protected Overridable Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
  End Sub

  ''' <summary>
  ''' Override this method to configure the model for this context.<br/>
  ''' This method is called once and the model is than cached for all <see cref="DbContext"/> instances of a particular SQL dialect.<br/>
  ''' The base implementation does nothing.
  ''' </summary>
  ''' <param name="modelBuilder"></param>
  Protected Overridable Sub OnModelCreating(modelBuilder As ModelBuilder)
  End Sub

  ''' <summary>
  ''' Override this method to intercept <see cref="DbCommand"/> executing.<br/>
  ''' Can be used e.g. to implement custom logging.<br/>
  ''' The base implementation does nothing.
  ''' </summary>
  ''' <param name="command"></param>
  Protected Overridable Sub OnCommandExecuting(command As DbCommand)
  End Sub

  ' TODO: SIP - add documentation.
  Public Function From(Of T)() As SelectSqlExpression(Of T)
    Return New SelectSqlExpression(Of T)(Me)
  End Function

  Public Function Execute(sql As FormattableString) As Int32
    Return (New SqlExpression(Me)).Execute(sql)
  End Function

  Public Function Execute(sql As RawSqlString) As Int32
    Return (New SqlExpression(Me)).Execute(sql)
  End Function

  Public Function QueryFirstOrDefault(Of T)(sql As FormattableString) As T
    Return (New SqlExpression(Me)).QueryFirstOrDefault(Of T)(sql)
  End Function

  Public Function QueryFirstOrDefault(Of T)(sql As RawSqlString) As T
    Return (New SqlExpression(Me)).QueryFirstOrDefault(Of T)(sql)
  End Function

  Public Function Query(Of T)(sql As FormattableString) As List(Of T)
    Return (New SqlExpression(Me)).Query(Of T)(sql)
  End Function

  Public Function Query(Of T)(sql As RawSqlString) As List(Of T)
    Return (New SqlExpression(Me)).Query(Of T)(sql)
  End Function

  Public Function Insert(Of T)(obj As T, Optional useDbIdentityAndDefaults As Boolean = True, Optional setAutoFields As Boolean = True) As Int32
    Return (New InsertSqlExpression(Of T)(Me, useDbIdentityAndDefaults, setAutoFields)).Insert(obj)
  End Function

  Public Function Update(Of T)(obj As T, Optional setAutoFields As Boolean = True) As Int32
    Return (New UpdateSqlExpression(Of T)(Me, setAutoFields)).Update(obj)
  End Function

  Public Function Update(Of T)(Optional setAutoFields As Boolean = True) As UpdateSqlExpression(Of T)
    Return New UpdateSqlExpression(Of T)(Me, setAutoFields)
  End Function

  Public Function Delete(Of T)(obj As T) As Int32
    Return (New DeleteSqlExpression(Of T)(Me, False)).Delete(obj)
  End Function

  Public Function Delete(Of T)() As DeleteSqlExpression(Of T)
    Return New DeleteSqlExpression(Of T)(Me, False)
  End Function

  Public Function SoftDelete(Of T)(obj As T) As Int32
    Return (New DeleteSqlExpression(Of T)(Me, True)).SoftDelete(obj)
  End Function

  Public Function SoftDelete(Of T)() As DeleteSqlExpression(Of T)
    Return New DeleteSqlExpression(Of T)(Me, True)
  End Function

#Region "IDisposable Support"
  Private m_DisposedValue As Boolean

  ''' <summary>
  ''' Releases all resources used by the <see cref="DbContext"/> class.
  ''' </summary>
  ''' <param name="disposing"></param>
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not m_DisposedValue Then
      If disposing Then
        If m_Database IsNot Nothing Then
          Me.Database.Dispose()
          m_Database = Nothing 'Database property shouldn't be accessed after this
        End If
      End If
    End If
    m_DisposedValue = True
  End Sub

  ''' <summary>
  ''' Releases all resources used by the <see cref="DbContext"/> class.
  ''' </summary>
  Public Sub Dispose() Implements IDisposable.Dispose
    Dispose(True)
  End Sub
#End Region

End Class
