import os
import json
import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    connection_string = os.getenv("AzureSignalRConnectionString", None)

    if connection_string is None or len(connection_string.strip()) == 0:
        return func.HttpResponse(
            json.dumps({
                "status": "fail",
                "env": {
                    "AzureSignalRConnectionString": connection_string
                }
            }), mimetype='application/json')

    return func.HttpResponse(
        json.dumps({"status": "ok"}), mimetype='application/json')
