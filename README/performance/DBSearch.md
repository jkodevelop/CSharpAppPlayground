/////////////////////////////////////////////////////////
MONGO simple search
/////////////////////////////////////////////////////////

--- Method 1: Search Using Contains + Case Insensitive ---
Found 830 LIKE searchTerm:aaa
[benchmark] Method 'Test_SearchContain' executed in 00:00:01.0664820 ms, SearchContain

--- Method 2: Search Using Contains + Case Sensitive ---
found number 97
Found 97 LIKE + case searchTerm:aaa
[benchmark] Method 'Test_SearchContainCaseSensitive' executed in 00:00:00.8361634 ms, SearchContainCaseSensitive

--- Method 3: Search Exact ---
found number 0
Found 0 EXACT searchTerm:aaa
[benchmark] Method 'Test_SearchExact' executed in 00:00:00.4394877 ms, SearchExact

/////////////////////////////////////////////////////////
MYSQL simple search
/////////////////////////////////////////////////////////

--- Method 1: Search Using LIKE ---
Found 830 LIKE searchTerm:aaa
[benchmark] Method 'Test_GetFilenameByLike' executed in 00:00:00.8135779 ms, GetFilenameByLike

--- Method 2: Search Using LIKE casesensitive ---
Found 97 LIKE + case searchTerm:aaa
[benchmark] Method 'Test_GetFilenameByLikeCaseSensitive' executed in 00:00:00.4763561 ms, GetFilenameByLikeCaseSensitive

--- Method 3: Search Exact ---
Found 0 EXACT searchTerm:aaa
[benchmark] Method 'Test_GetFilename' executed in 00:00:00.0035428 ms, GetFilename

/////////////////////////////////////////////////////////
POSTGRES simple search
/////////////////////////////////////////////////////////

--- Method 1: Search Using LIKE ---
Found 96 LIKE searchTerm:aaa
[benchmark] Method 'Test_GetFilenameByLike' executed in 00:00:00.1092882 ms, GetFilenameByLike

--- Method 2: Search Using LIKE + case sensitive ---
Found 96 LIKE + case searchTerm:aaa
[benchmark] Method 'Test_GetFilenameByLikeCaseSensitive' executed in 00:00:00.0581573 ms, GetFilenameByLikeCaseSensitive

--- Method 3: Search Exact ---
Found 0 EXACT searchTerm:aaa
[benchmark] Method 'Test_GetFilename' executed in 00:00:00.0031357 ms, GetFilename
