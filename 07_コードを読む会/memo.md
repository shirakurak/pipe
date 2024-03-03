# 整理メモ

[activerecord](https://github.com/rails/rails/tree/main/activerecord)配下のマイグレーションに関する箇所を探す

## 当座の目標

schema_migrationテーブルにタイムスタンプのレコードを追加する箇所はどこか？

## [activerecord/lib/rails/generators/active_record/migration](https://github.com/rails/rails/tree/main/activerecord/lib/rails/generators/active_record/migration)

ここは、generatorコマンドに関する箇所

## [activerecord/lib/active_record/migration](https://github.com/rails/rails/tree/main/activerecord/lib/active_record/migration)

- command_recorder.rb
- compatibility.rb
- default_strategy.rb
- execution_strategy.rb
- join_table.rb
- pending_migration_connection.rb

## activerecord/lib/active_record/migration.rb

### アウトライン

- エラー系
  - MigrationError < ActiveRecordError
  - IrreversibleMigration < MigrationError
  - DuplicateMigrationVersionError < MigrationError
  - DuplicateMigrationNameError < MigrationError
  - UnknownMigrationVersionError < MigrationError
  - IllegalMigrationNameError < MigrationError
  - InvalidMigrationTimestampError < MigrationError
  - PendingMigrationError < MigrationError
  - ConcurrentMigrationError < MigrationError
  - NoEnvironmentInSchemaError < MigrationError
  - ProtectedEnvironmentError < ActiveRecordError
  - EnvironmentMismatchError < ActiveRecordError
  - EnvironmentStorageError < ActiveRecordError
- Migration
- MigrationProxy
- MigrationContext
- Migrator

### 流れを追う

`MigrationContext.migrate`

↓

`MigrationContext.up`

`MigrationContext.down`

↓

`Migrator.run`

`Migrator.migrate`

↓

`Migrator.run_without_lock`

`Migrator.migrate_without_lock`

↓

`Migrator.execute_migration_in_transaction`

```rb
def execute_migration_in_transaction(migration)
  return if down? && !migrated.include?(migration.version.to_i)
  return if up?   &&  migrated.include?(migration.version.to_i)

  Base.logger.info "Migrating to #{migration.name} (#{migration.version})" if Base.logger

  ddl_transaction(migration) do
    migration.migrate(@direction)
    record_version_state_after_migrating(migration.version)
  end
rescue => e
  msg = +"An error has occurred, "
  msg << "this and " if use_transaction?(migration)
  msg << "all later migrations canceled:\n\n#{e}"
  raise StandardError, msg, e.backtrace
end
```

↓

`Migrator.record_version_state_after_migrating`

```rb
def record_version_state_after_migrating(version)
  if down?
    migrated.delete(version)
    @schema_migration.delete_version(version.to_s)
  else
    migrated << version
    @schema_migration.create_version(version.to_s)
  end
end
```
