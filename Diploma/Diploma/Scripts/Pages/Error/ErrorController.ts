export class ErrorController {
    public static $inject: string[] =
    [
        '$routeParams'
    ];

    public status: number;
    public message: string;

    constructor(
        params: ng.route.IRouteParamsService
    ) {
        this.status = params['status'];
        this.message = params['message'];
    }
}