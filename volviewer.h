
// volviewer.h: archivo de encabezado principal para la aplicaci�n volviewer
//
#pragma once

#ifndef __AFXWIN_H__
	#error "incluir 'stdafx.h' antes de incluir este archivo para PCH"
#endif

#include "resource.h"       // S�mbolos principales


// CvolviewerApp:
// Consulte la secci�n volviewer.cpp para obtener informaci�n sobre la implementaci�n de esta clase
//

class CvolviewerApp : public CWinApp
{
public:
	CvolviewerApp();


// Reemplazos
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementaci�n

public:
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CvolviewerApp theApp;
