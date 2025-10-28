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
