

check/check: check/*.cpp check/*.h
	@cd check; c++ -w -o check check.cpp
