 After cold start + flushing the windows file count caching
 TO test performance need to eliminate as much caching as possible.
 
 The biggest problem with this test is there are multiple layers and hardware services from the computer and the external drive that optimize for the user
 So for external drive file count test (file seeking), The only way is to unmount and remount and test one function at a time.
 The goal is to find the fastest count method/file looping method out of all possible solutions. And compare.
 Obviously there are multiple API and based on the current date that could change.
 I don't know all possible APIs for file count in .Net
 In the future the test should also be tested against different languages as well like C++ and Python or Rust, etc...
 BUT for now I am trying to create a .net WinForm desktop app so need to test performance based on C# language.

## SUMMARY

### CountFilesFolder.cs

TOTAL => CountMethodJ(F:\_): file=103374, folder=5796
Method 'Test_CountMethodJ' executed in 00:00:59.8989120 ms, CountMethodJ: F:\_
TOTAL => CountMethodJ(F:\_): file=103374, folder=5796
Method 'Test_CountMethodJ' executed in 00:00:32.5763375 ms, CountMethodJ: F:\_
TOTAL => CountMethodJ(F:\_): file=103374, folder=5796
Method 'Test_CountMethodJ' executed in 00:00:58.9567948 ms, CountMethodJ: F:\_

TOTAL => CountMethodF(F:\_): file=103374, folder=5796
Method 'Test_CountMethodF' executed in 00:00:59.1210387 ms, CountMethodF: F:\_

TOTAL => CountMethodJ_Alt(F:\_): file=103374, folder=5796
Method 'Test_CountMethodJAlt' executed in 00:00:58.9792348 ms, CountMethodJ_Alt: F:\_
TOTAL => CountMethodJ_Alt(F:\_): file=103374, folder=5796
Method 'Test_CountMethodJAlt' executed in 00:00:58.9966800 ms, CountMethodJ_Alt: F:\_

TOTAL => CountMethodH(F:\_): file=103374, folder=5796
Method 'Test_CountMethodH' executed in 00:01:08.4183634 ms, CountMethodH: F:\_

TOTAL => CountMethodI(F:\_): file=103374, folder=5796
Method 'Test_CountMethodI' executed in 00:01:07.1059507 ms, CountMethodI: F:\_