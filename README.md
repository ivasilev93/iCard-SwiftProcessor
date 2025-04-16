# Task Description
## Develop an application (console or service) that processes credit transfers in SWIFT MT103 format.

The application must:

Continuously monitor a configurable local folder for new .TXT files containing MT103 messages.

Parse each valid file into structured objects.

Store the following in a MS SQL database:

File metadata (name, timestamp, number of transfers).

Transfer data (amount, currency, description, sender/receiver name, account, BIC).

Validation:

If any transfer in a file is missing critical fields (reference, sender/receiver account), the file must not be imported.

Instead, move it to a FAILED subfolder and log the error.

On successful processing, move the file to a SUCCESS subfolder and log the event.
