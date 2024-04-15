import { environment } from "src/environments/environment";

if (environment.production == true)
    var url = '/api';
else
    var url = 'http://localhost:6600/api';

export const baseURL: string = url;