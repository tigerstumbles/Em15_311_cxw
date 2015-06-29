/*
 * tmp.h
 *
 *  Created on: 2015-6-28
 *      Author: suoni
 */


#include "xparameters.h"
#include "i2c.h"

#define	Tmp2IICAddr	0x4B

//#define	Tmp3IICAddr	0x48
#define	TmpDataReg	0x0
#define	TmpDataLReg	0x1
//#define	TmpCfgReg	0x1

#define I2C_BASEADDR XPAR_AXI_IIC_0_BASEADDR

int	Read_Tmp(int print);
