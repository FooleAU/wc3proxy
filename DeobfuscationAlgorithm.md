# Introduction #

Some parts of the query packets are obfuscated/encrypted using a very simple algorithm.  Basically every 8 bytes (starting at the first) is a set of flags which indicate which of the following 7 bytes have been increased by 1.

# Pseudocode #
  * Take the next byte from the source.  If it is zero, stop.

byte b = Data`[`pos + Offset`]`;

if (b == 0) break;

  * If the offset is a multiple of 8, store its value.

if (pos % 8 == 0) mask = b;

  * For all other bytes, find the corresponding bit in the flags byte.

flag = mask & (0x1 << (pos % 8))

  * If it is zero, subtract 1 from the value and append it to the result

output.WriteByte((byte)(b - 1));

  * Otherwise, just append it to the result

output.WriteByte(b);

## Example 1 ##

2f 73 65 5f 77 33 2f 31

0x2f = 00101111b.  Reverse the order, skip the first one and the match them to the following bytes

Output:

73 65 5F **76** 33 **2e** **30**

## Example 2 ##

39 79 2f 77 33 6d 01 5b

0x39 = 00111001b.

Output:

**78** **2e** 77 33 6d **00** **5a**