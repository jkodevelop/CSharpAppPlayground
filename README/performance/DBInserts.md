=== MySQL Bulk Insert Examples with VidsSQL ===

Inserted 10000 records using Single Loop

[benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:21.8211869 ms, BulkInsertSingleLoop:

--- Method 2: Multi-Value INSERT Statement ---
Dataset size 10000 exceeds overload limit 10000, skipping Multi-Value Insert to avoid packet size issues.
Inserted -1 records using Multi-Value

[benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0064607 ms, BulkInsertMultiValue:

--- Method 3: Transaction with Batched Inserts ---
Inserted 10000 records using Transaction with Batching

[benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:01.2863570 ms, BulkInsertWithTransaction:

--- Method 4: Prepared Statement with Batching ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:21.3598947 ms, BulkInsertWithPreparedStatement:

--- Method 5: Prepared Statement with Batching + transaction ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.9789103 ms, BulkInsertWithPreparedStatementAndTransaction:

=== Benchmark Complete ===
Inserted:40000

///////////////////////////////////////////////

--- Method 1: Single Insert Loop ---
Inserted 10000 records using Single Loop

[benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:22.2732613 ms, BulkInsertSingleLoop:

--- Method 2: Multi-Value INSERT Statement ---
Dataset size 10000 exceeds overload limit 10000, skipping Multi-Value Insert to avoid packet size issues.
Inserted -1 records using Multi-Value

[benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0040632 ms, BulkInsertMultiValue:

--- Method 3: Transaction with Batched Inserts ---
Inserted 10000 records using Transaction with Batching

[benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:01.2086543 ms, BulkInsertWithTransaction:

--- Method 4: Prepared Statement with Batching ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:21.5731471 ms, BulkInsertWithPreparedStatement:

--- Method 5: Prepared Statement with Batching + transaction ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:01.0892831 ms, BulkInsertWithPreparedStatementAndTransaction:

=== Benchmark Complete ===
Inserted:40000



--- Method 1: Single Insert Loop ---
Inserted 10000 records using Single Loop

[benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:22.5612252 ms, BulkInsertSingleLoop:

--- Method 2: Multi-Value INSERT Statement ---
Inserted 10000 records using Multi-Value

[benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.2262151 ms, BulkInsertMultiValue:

--- Method 3: Transaction with Batched Inserts ---
The thread '.NET TP Worker' (35604) has exited with code 0 (0x0).
Inserted 10000 records using Transaction with Batching

[benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:01.2479735 ms, BulkInsertWithTransaction:

--- Method 4: Prepared Statement with Batching ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:22.1105986 ms, BulkInsertWithPreparedStatement:

--- Method 5: Prepared Statement with Batching + transaction ---
Inserted 10000 records using Prepared Statement with Batching

[benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.9397945 ms, BulkInsertWithPreparedStatementAndTransaction:

=== Benchmark Complete ===
Inserted:50000



--- Method 1: Single Insert Loop ---
Inserted 25000 records using Single Loop

[benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:55.4211518 ms, BulkInsertSingleLoop:

--- Method 2: Multi-Value INSERT Statement ---
Inserted 25000 records using Multi-Value

[benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.5340486 ms, BulkInsertMultiValue:

--- Method 3: Transaction with Batched Inserts ---
Inserted 25000 records using Transaction with Batching

[benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:03.0286370 ms, BulkInsertWithTransaction:

--- Method 4: Prepared Statement with Batching ---
Inserted 25000 records using Prepared Statement with Batching

[benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:54.8843319 ms, BulkInsertWithPreparedStatement:

--- Method 5: Prepared Statement with Batching + transaction ---
Inserted 25000 records using Prepared Statement with Batching

[benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:02.4687558 ms, BulkInsertWithPreparedStatementAndTransaction:

=== Benchmark Complete ===
Inserted:125000


////////// speed rounds

-- 1 entry

[2025-11-07 12:25:41 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:00.0176230 ms, BulkInsertSingleLoop:
//[2025-11-07 12:25:41 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0067206 ms, BulkInsertMultiValue:
//[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:00.0067215 ms, BulkInsertWithTransaction:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:00.0136030 ms, BulkInsertWithPreparedStatement:
//[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.0057694 ms, BulkInsertWithPreparedStatementAndTransaction:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:00.0105897 ms, BulkInsertUseCSVOperation:


[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:00.0255422 ms, BulkInsertSingleLoop:
//[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0035444 ms, BulkInsertMultiValue:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:00.0058666 ms, BulkInsertWithTransaction:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:00.0121931 ms, BulkInsertWithPreparedStatement:
//[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.0045353 ms, BulkInsertWithPreparedStatementAndTransaction:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:00.0073008 ms, BulkInsertUseCopyCommand:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:00.0114862 ms, BulkInsertUseBinaryImport:
[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkAddWithPgPartner' executed in 00:00:00.1436817 ms, BulkAddWithPgPartner:


[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:00.0549838 ms, InsertSimpleLoop:
//[2025-11-07 12:25:42 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:00.0037872 ms, InsertManyAPI:


-- 1000 entry

[2025-11-07 1:04:47 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:02.5046135 ms, BulkInsertSingleLoop:
//[2025-11-07 1:04:47 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0517555 ms, BulkInsertMultiValue:
[2025-11-07 1:04:48 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:00.1512975 ms, BulkInsertWithTransaction:
[2025-11-07 1:04:50 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:02.8160877 ms, BulkInsertWithPreparedStatement:
[2025-11-07 1:04:50 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.1120927 ms, BulkInsertWithPreparedStatementAndTransaction:
//[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:00.0290250 ms, BulkInsertUseCSVOperation:


[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:00.2881752 ms, BulkInsertSingleLoop:
//[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0244894 ms, BulkInsertMultiValue:
[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:00.1085572 ms, BulkInsertWithTransaction:
[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:00.1482922 ms, BulkInsertWithPreparedStatement:
//[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:00.0721791 ms, BulkInsertWithPreparedStatementAndTransaction:
//[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:00.0166817 ms, BulkInsertUseCopyCommand:
//[2025-11-07 1:04:51 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:00.0222412 ms, BulkInsertUseBinaryImport:
[2025-11-07 1:04:52 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkAddWithPgPartner' executed in 00:00:00.5082651 ms, BulkAddWithPgPartner:


[2025-11-07 1:04:52 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:00.4064801 ms, InsertSimpleLoop:
//[2025-11-07 1:04:52 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:00.0116922 ms, InsertManyAPI:


-- 50000 entry

[2025-11-07 1:12:27 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:01:48.4314076 ms, BulkInsertSingleLoop:
[2025-11-07 1:12:27 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0067433 ms, BulkInsertMultiValue:
[2025-11-07 1:12:33 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:06.0844086 ms, BulkInsertWithTransaction:
[2025-11-07 1:14:20 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:01:47.0240674 ms, BulkInsertWithPreparedStatement:
[2025-11-07 1:14:25 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:04.9901116 ms, BulkInsertWithPreparedStatementAndTransaction:
//[2025-11-07 1:14:26 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:00.9495428 ms, BulkInsertUseCSVOperation:


[2025-11-07 1:14:35 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:08.4980740 ms, BulkInsertSingleLoop:
[2025-11-07 1:14:35 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertMultiValue' executed in 00:00:00.0051864 ms, BulkInsertMultiValue:
[2025-11-07 1:14:39 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithTransaction' executed in 00:00:03.9145873 ms, BulkInsertWithTransaction:
[2025-11-07 1:14:45 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertWithPreparedStatement' executed in 00:00:06.1306328 ms, BulkInsertWithPreparedStatement:
[2025-11-07 1:14:48 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertWithPreparedStatementAndTransaction' executed in 00:00:02.8328655 ms, BulkInsertWithPreparedStatementAndTransaction:
/[2025-11-07 1:14:49 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:00.6422869 ms, BulkInsertUseCopyCommand:
//[2025-11-07 1:14:49 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:00.6574586 ms, BulkInsertUseBinaryImport:
[2025-11-07 1:15:00 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkAddWithPgPartner' executed in 00:00:10.4516677 ms, BulkAddWithPgPartner:



[2025-11-07 1:14:59 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertSimpleLoop' executed in 00:00:09.8918960 ms, InsertSimpleLoop:
//[2025-11-07 1:15:00 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:00.2242461 ms, InsertManyAPI:


-- 1000000

[2025-11-07 2:32:18 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:23.1536226 ms, BulkInsertUseCSVOperation:

[2025-11-07 2:32:35 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:15.6881985 ms, BulkInsertUseBinaryImport:
[2025-11-07 2:32:56 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:20.2075311 ms, BulkInsertUseCopyCommand:

[2025-11-07 2:33:01 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:04.4683517 ms, InsertManyAPI:

-- 1000000(1)

[2025-11-07 3:13:43 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:24.4068763 ms, BulkInsertUseCSVOperation:

[2025-11-07 3:13:59 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:15.5109157 ms, BulkInsertUseBinaryImport:
[2025-11-07 3:14:21 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:20.2952188 ms, BulkInsertUseCopyCommand:

[2025-11-07 3:14:43 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:21.0693093 ms, InsertManyAPI:

-- 1000000(2)

[2025-11-07 3:27:25 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:03:43.1187038 ms, BulkInsertUseCSVOperation:

[2025-11-07 3:27:55 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:29.3620678 ms, BulkInsertUseBinaryImport:
[2025-11-07 3:28:40 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:42.9722347 ms, BulkInsertUseCopyCommand:

[2025-11-07 3:29:04 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:23.3716018 ms, InsertManyAPI:

-- (3)

[2025-11-07 4:12:01 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCSVOperation' executed in 00:00:26.5409292 ms, BulkInsertUseCSVOperation:

[2025-11-07 4:12:16 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseBinaryImport' executed in 00:00:13.8964975 ms, BulkInsertUseBinaryImport:
[2025-11-07 4:12:35 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_BulkInsertUseCopyCommand' executed in 00:00:17.8982875 ms, BulkInsertUseCopyCommand:

[2025-11-07 4:12:57 PM] [Information] [CSharpAppPlayground] [benchmark] Method 'Test_InsertManyAPI' executed in 00:00:21.3805110 ms, InsertManyAPI: