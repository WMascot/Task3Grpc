#!/usr/bin/env bash

GRPCSERVER=localhost:5238
PROTODIR=./Protos

echo "List users. Empty balances."
grpcurl -import-path $PROTODIR -proto billing.proto -plaintext -d \
'{}' $GRPCSERVER billing.Billing/ListUsers

echo "Emission 10 coins."
grpcurl -import-path $PROTODIR -proto billing.proto -plaintext -d \
'{"amount": "111111"}' $GRPCSERVER billing.Billing/CoinsEmission

echo "List users. After the emission."
grpcurl -import-path $PROTODIR -proto billing.proto -plaintext -d \
'{}' $GRPCSERVER billing.Billing/ListUsers