Imports Yamo.Expressions.Builders
Imports Yamo.Infrastructure
Imports Yamo.Internal.Query
Imports Yamo.Metadata

Namespace Internal

  Public Class EntitySqlStringProviderCache

    Private Shared m_Instances As Dictionary(Of (SqlDialectProvider, Model), EntitySqlStringProviderCache)

    Private m_InsertProviders As Dictionary(Of Type, Func(Of Object, Boolean, CreateInsertSqlStringResult))

    Private m_UpdateProviders As Dictionary(Of Type, Func(Of Object, SqlString))

    Private m_DeleteProviders As Dictionary(Of Type, Func(Of Object, SqlString))

    Private m_SoftDeleteProviders As Dictionary(Of Type, Func(Of Object, SqlString))

    Private m_SoftDeleteWithoutConditionProviders As Dictionary(Of Type, Func(Of Object(), SqlString))

    Shared Sub New()
      m_Instances = New Dictionary(Of (SqlDialectProvider, Model), EntitySqlStringProviderCache)
    End Sub

    Private Sub New()
      m_InsertProviders = New Dictionary(Of Type, Func(Of Object, Boolean, CreateInsertSqlStringResult))
      m_UpdateProviders = New Dictionary(Of Type, Func(Of Object, SqlString))
      m_DeleteProviders = New Dictionary(Of Type, Func(Of Object, SqlString))
      m_SoftDeleteProviders = New Dictionary(Of Type, Func(Of Object, SqlString))
      m_SoftDeleteWithoutConditionProviders = New Dictionary(Of Type, Func(Of Object(), SqlString))
    End Sub

    Public Shared Function GetInsertProvider(builder As InsertSqlExpressionBuilder, useDbIdentityAndDefaults As Boolean, type As Type) As Func(Of Object, Boolean, CreateInsertSqlStringResult)
      Return GetInstance(builder.DialectProvider, builder.DbContext.Model).GetOrCreateInsertProvider(builder, useDbIdentityAndDefaults, type)
    End Function

    Public Shared Function GetUpdateProvider(builder As UpdateSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Return GetInstance(builder.DialectProvider, builder.DbContext.Model).GetOrCreateUpdateProvider(builder, type)
    End Function

    Public Shared Function GetDeleteProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Return GetInstance(builder.DialectProvider, builder.DbContext.Model).GetOrCreateDeleteProvider(builder, type)
    End Function

    Public Shared Function GetSoftDeleteProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Return GetInstance(builder.DialectProvider, builder.DbContext.Model).GetOrCreateSoftDeleteProvider(builder, type)
    End Function

    Public Shared Function GetSoftDeleteWithoutConditionProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object(), SqlString)
      Return GetInstance(builder.DialectProvider, builder.DbContext.Model).GetOrCreateSoftDeleteWithoutConditionProvider(builder, type)
    End Function

    Private Shared Function GetInstance(dialectProvider As SqlDialectProvider, model As Model) As EntitySqlStringProviderCache
      Dim instance As EntitySqlStringProviderCache = Nothing

      Dim key = (dialectProvider, model)

      SyncLock m_Instances
        If Not m_Instances.TryGetValue(key, instance) Then
          instance = New EntitySqlStringProviderCache
          m_Instances.Add(key, instance)
        End If
      End SyncLock

      Return instance
    End Function

    Private Function GetOrCreateInsertProvider(builder As InsertSqlExpressionBuilder, useDbIdentityAndDefaults As Boolean, type As Type) As Func(Of Object, Boolean, CreateInsertSqlStringResult)
      Dim provider As Func(Of Object, Boolean, CreateInsertSqlStringResult) = Nothing

      SyncLock m_InsertProviders
        m_InsertProviders.TryGetValue(type, provider)
      End SyncLock

      If provider Is Nothing Then
        provider = builder.DialectProvider.EntitySqlStringProviderFactory.CreateInsertProvider(builder, useDbIdentityAndDefaults, type)
      Else
        Return provider
      End If

      SyncLock m_InsertProviders
        m_InsertProviders(type) = provider
      End SyncLock

      Return provider
    End Function

    Private Function GetOrCreateUpdateProvider(builder As UpdateSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Dim provider As Func(Of Object, SqlString) = Nothing

      SyncLock m_UpdateProviders
        m_UpdateProviders.TryGetValue(type, provider)
      End SyncLock

      If provider Is Nothing Then
        provider = builder.DialectProvider.EntitySqlStringProviderFactory.CreateUpdateProvider(builder, type)
      Else
        Return provider
      End If

      SyncLock m_UpdateProviders
        m_UpdateProviders(type) = provider
      End SyncLock

      Return provider
    End Function

    Private Function GetOrCreateDeleteProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Dim provider As Func(Of Object, SqlString) = Nothing

      SyncLock m_DeleteProviders
        m_DeleteProviders.TryGetValue(type, provider)
      End SyncLock

      If provider Is Nothing Then
        provider = builder.DialectProvider.EntitySqlStringProviderFactory.CreateDeleteProvider(builder, type)
      Else
        Return provider
      End If

      SyncLock m_DeleteProviders
        m_DeleteProviders(type) = provider
      End SyncLock

      Return provider
    End Function

    Private Function GetOrCreateSoftDeleteProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object, SqlString)
      Dim provider As Func(Of Object, SqlString) = Nothing

      SyncLock m_SoftDeleteProviders
        m_SoftDeleteProviders.TryGetValue(type, provider)
      End SyncLock

      If provider Is Nothing Then
        provider = builder.DialectProvider.EntitySqlStringProviderFactory.CreateSoftDeleteProvider(builder, type)
      Else
        Return provider
      End If

      SyncLock m_SoftDeleteProviders
        m_SoftDeleteProviders(type) = provider
      End SyncLock

      Return provider
    End Function

    Private Function GetOrCreateSoftDeleteWithoutConditionProvider(builder As DeleteSqlExpressionBuilder, type As Type) As Func(Of Object(), SqlString)
      Dim provider As Func(Of Object(), SqlString) = Nothing

      SyncLock m_SoftDeleteWithoutConditionProviders
        m_SoftDeleteWithoutConditionProviders.TryGetValue(type, provider)
      End SyncLock

      If provider Is Nothing Then
        provider = builder.DialectProvider.EntitySqlStringProviderFactory.CreateSoftDeleteWithoutConditionProvider(builder, type)
      Else
        Return provider
      End If

      SyncLock m_SoftDeleteWithoutConditionProviders
        m_SoftDeleteWithoutConditionProviders(type) = provider
      End SyncLock

      Return provider
    End Function

  End Class
End Namespace