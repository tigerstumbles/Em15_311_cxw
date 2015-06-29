


#include "tmp.h"

static int count=0;

int	Read_Tmp(int print)
{
	unsigned	char	rxBuffer[2];
	unsigned	int	data;
	I2C_Read(I2C_BASEADDR,Tmp2IICAddr,TmpDataReg,1,rxBuffer);
	data=rxBuffer[0];
	I2C_Read(I2C_BASEADDR,Tmp2IICAddr,TmpDataLReg,1,rxBuffer);
	data=data<<8|rxBuffer[0];
	int tmp=(data>>3)*0.0625;
	/*if(print)
		xil_printf("Temperature is:%d degrees Celsius\r\n",tmp);*/
	count++;
	xil_printf("Temperature is:%2d degrees Celsius\r\n",count);
	return	count;
}
