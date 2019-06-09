Imports System.Data.Common
Imports Yamo.Expressions
Imports Yamo.Infrastructure
Imports Yamo.Internal
Imports Yamo.Metadata

Public Class DbContext
  Implements IDisposable

  Private m_Model As Model
  Public ReadOnly Property Model As Model
    Get
      If m_Model Is Nothing Then
        m_Model = ModelCache.GetModel(Me.Options.DialectProvider, Me)
      End If

      Return m_Model
    End Get
  End Property

  Private m_Options As DbContextOptions
  Friend ReadOnly Property Options As DbContextOptions
    Get
      If m_Options Is Nothing Then
        CreateOptions()
      End If

      Return m_Options
    End Get
  End Property

  Private m_Database As DatabaseFacade
  Public ReadOnly Property Database As DatabaseFacade
    Get
      If m_Database Is Nothing Then
        CreateDatabase()
      End If

      Return m_Database
    End Get
  End Property

  Sub New()
  End Sub

  Friend Function CreateModel() As Model
    Dim modelBuilder = New ModelBuilder
    OnModelCreating(modelBuilder)
    Return modelBuilder.Model()
  End Function

  Private Sub CreateOptions()
    Dim optionsBuilder = New DbContextOptionsBuilder
    OnConfiguring(optionsBuilder)
    m_Options = optionsBuilder.Options
  End Sub

  Private Sub CreateDatabase()
    m_Database = New DatabaseFacade(Me)
  End Sub

  Friend Sub NotifyCommandExecution(command As DbCommand)
    OnCommandExecuting(command)
  End Sub

  Protected Overridable Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
  End Sub

  Protected Overridable Sub OnModelCreating(modelBuilder As ModelBuilder)
  End Sub

  Protected Overridable Sub OnCommandExecuting(command As DbCommand)
  End Sub

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

  Public Sub Dispose() Implements IDisposable.Dispose
    Dispose(True)
  End Sub
#End Region

End Class
