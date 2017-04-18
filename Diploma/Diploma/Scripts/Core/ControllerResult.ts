export class ControllerResult<TValue>
{
    public isSuccess: boolean;

    public message: string;

    public status: number;

    public value: TValue;
}