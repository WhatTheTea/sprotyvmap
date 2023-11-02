# Welcome to Cloud Functions for Firebase for Python!
# To get started, simply uncomment the below code or create your own.
# Deploy with `firebase deploy`
from firebase_functions import https_fn
from firebase_admin import initialize_app
from sprotyvmap_api import sm_api

initialize_app()

@https_fn.on_request()
def flask_request(req: https_fn.Request) -> https_fn.Response:
    with  sm_api.api.request_context(req.environ):
        return sm_api.api.full_dispatch_request()