
Feature: The Berlin Clock
	As a clock enthusiast
    I want to tell the time using the Berlin Clock
    So that I can increase the number of ways that I can read the time


Scenario: Midnight 00:00
When the time is "00:00:00"
Then the clock should look like
"""
Y
OOOO
OOOO
OOOOOOOOOOO
OOOO
"""


Scenario: Middle of the afternoon
When the time is "13:17:01"
Then the clock should look like
"""
O
RROO
RRRO
YYROOOOOOOO
YYOO
"""

Scenario: Just before midnight
When the time is "23:59:59"
Then the clock should look like
"""
O
RRRR
RRRO
YYRYYRYYRYY
YYYY
"""

Scenario: Midnight 24:00
When the time is "24:00:00"
Then the clock should look like
"""
Y
RRRR
RRRR
OOOOOOOOOOO
OOOO
"""

Scenario: Immediately after midnight
When the time is "24:00:01"
Then I am notified of error

Scenario: Negative time
When the time is "-01:00:01"
Then I am notified of error

Scenario: Too detailed time
When the time is "00:00:00:00"
Then I am notified of error

Scenario: Badly formatted time
When the time is "00:bbb:ccc"
Then I am notified of error

Scenario: Hours exceed range
When the time is "25:00:00"
Then I am notified of error

Scenario: Minutes exceed range
When the time is "00:60:00"
Then I am notified of error

Scenario: Seconds exceed range
When the time is "00:00:60"
Then I am notified of error
