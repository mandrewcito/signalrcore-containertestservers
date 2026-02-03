import os
import json
import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    connection_string = os.getenv("SIGNALRCORE_AZURE_CONNECTION_STRING", None)

    if connection_string is None or len(connection_string.strip()) == 0:
        return func.HttpResponse(
            json.dumps({
                "status": "fail",
                "env": {
                    "SIGNALRCORE_AZURE_CONNECTION_STRING": connection_string
                }
            }), mimetype='application/json')

    return func.HttpResponse(
        json.dumps({"status": "ok"}), mimetype='application/json')
