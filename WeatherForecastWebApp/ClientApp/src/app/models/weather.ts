export interface Weather {
  dateTime: Date;
  description: string | undefined;
  icon: string | undefined;
  temperature: number;
}
