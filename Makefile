SHELL := /bin/bash


.PHONY: exe
exe:
	csc AccountHierarchyTest.cs Account.cs CheckingAccount.cs SavingsAccount.cs


.PHONY: output
output: exe
	mono AccountHierarchyTest.exe | tee AccountHierarchyTestOutput.txt


.PHONY: test
test: output
	@diff expected.out InvoiceLINQOutput.txt


.PHONY: help
help:
	@printf "available targets -->\n\n"
	@cat Makefile | grep ".PHONY" | grep -v ".PHONY: _" | sed 's/.PHONY: //g'

