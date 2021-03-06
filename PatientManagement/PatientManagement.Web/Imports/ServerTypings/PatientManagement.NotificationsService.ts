﻿namespace PatientManagement.PatientManagement {
    export namespace NotificationsService {
        export const baseUrl = 'PatientManagement/Notifications';

        export declare function Create(request: Serenity.SaveRequest<NotificationsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<NotificationsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<NotificationsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NotificationsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ListForDropdown(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NotificationsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function CountNotifications(request: Serenity.ListRequest, onSuccess?: (response: CountNotificationsResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export namespace Methods {
            export declare const Create: string;
            export declare const Update: string;
            export declare const Delete: string;
            export declare const Retrieve: string;
            export declare const List: string;
            export declare const ListForDropdown: string;
            export declare const CountNotifications: string;
        }

        ['Create', 'Update', 'Delete', 'Retrieve', 'List', 'ListForDropdown', 'CountNotifications'].forEach(x => {
            (<any>NotificationsService)[x] = function (r, s, o) { return Q.serviceRequest(baseUrl + '/' + x, r, s, o); };
            (<any>Methods)[x] = baseUrl + '/' + x;
        });
    }
}
