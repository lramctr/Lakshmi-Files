#-------------------------------------------------------------------------------
# Name:         Utils
# Purpose:      Utils
#
# Author:       Amritpal Kang
#
# Created:      09/30/2015
# Copyright:    (c) Amritpal Kang 2015
#-------------------------------------------------------------------------------

import arcpy
import sys
import traceback

class Utils:
    
    schemaType = "NO_TEST"
    fieldMappings = ""
    subtype = ""

    def __init__(self, arcSdeConn, devMode):
        """ Constructor """
        
        self.arcSdeConn = arcSdeConn
        self.devMode = devMode
    
    """ This class contains common methods """
    def createDataset(self):
        """ Creates new dataset """    
        methodName = self.createDataset.__name__
        try:
            # GCS_North_American_1983
            spRef = arcpy.SpatialReference(4269)
            arcpy.CreateFeatureDataset_management(self.arcSdeConn, "FEATURES", spRef)
    
            self.addMessage("Feature dataset created")
        except arcpy.ExecuteError:
            self.addError(methodName, arcpy.GetMessages(2))
        except Exception as e:
            self.addError(methodName, e.args[0])
    
    def createFeatClasses(self, featClasses, globalId=True):
        """ Creates new feature classes """    
        methodName = self.createFeatClasses.__name__
        try:
            featDset = "{0}\FEATURES".format(self.arcSdeConn)
            for featClass in featClasses:
                arcpy.CreateFeatureclass_management(featDset, featClass, 
                    featClasses[featClass]["Geometry"], 
                    has_m=featClasses[featClass]["HasM"])
                featClassPath = "{0}\{1}".format(featDset, featClass)
                if(globalId):
                    arcpy.AddGlobalIDs_management(featClassPath)
                self.addMessage("Feature class {0} created".format(featClass))
                
            self.addMessage("All feature classes created")
        except arcpy.ExecuteError:
            self.addError(methodName, arcpy.GetMessages(2))
        except Exception as e:
            self.addError(methodName, e.args[0])
            
    def createTables(self, tableList, globalId=True):
        """ Creates new tables  """
        methodName = self.createTables.__name__
        try:
            for table in tableList:
                arcpy.CreateTable_management(self.arcSdeConn, table)
                tablePath = "{0}\{1}".format(self.arcSdeConn, table)
                if(globalId):
                    arcpy.AddGlobalIDs_management(tablePath)
                self.addMessage("Table {0} created".format(table))
            
            self.addMessage("All tables created")
        except arcpy.ExecuteError:
            self.addError(methodName, arcpy.GetMessages(2))
        except Exception as e:
            self.addError(methodName, e.args[0])
    
    def loadFeatClasses(self, tempGdbPath, featClasses):
        """ 
        Creates new feature classes 
        
        Args:
            tempGdbPath: Temp Geodatabase Path
        """    
        methodName = self.loadFeatClasses.__name__
        try:
            sdeFeatDset = "{0}\FEATURES".format(self.arcSdeConn)
            tempFeatDset= "{0}\FEATURES".format(tempGdbPath)
            
            for featClass in featClasses:
                sdeFeatClassPath = "{0}\{1}".format(sdeFeatDset, featClass)
                tempFeatClassPath = "{0}\{1}".format(tempFeatDset, featClass)
                arcpy.Append_management(tempFeatClassPath, sdeFeatClassPath, 
                    self.schemaType, self.fieldMappings, self.subtype)
                
                self.addMessage("Feature class {0} data added.".format(featClass))
            
            self.addMessage("Data added to all feature classes.")
        except arcpy.ExecuteError:
            self.addError(methodName, arcpy.GetMessages(2))
        except Exception as e:
            self.addError(methodName, e.args[0])
        
    def loadTables(self, tempGdbPath, tableList):
        """ 
        Creates new tables  
        
        Args:
            tempGdbPath: Temp Geodatabase Path
        """
        methodName = self.loadTables.__name__
        try:
            for table in tableList:
                tempTablePath = "{0}\{1}".format(tempGdbPath, table)
                arcpy.Append_management(tempTablePath, table, 
                    self.schemaType, self.fieldMappings, self.subtype)
                
                self.addMessage("Table {0} data added.".format(table))
            
            self.addMessage("Data added to all tables.")
        except arcpy.ExecuteError:
            self.addError(methodName, arcpy.GetMessages(2))
        except Exception as e:
            self.addError(methodName, e.args[0])
          
    def addMessage(self, message, messageType="Message"):
        """
        Adds message to output
    
        Args:
            message: Message to be displayed
            messageType: Message(Default) or Warning
        """
        if(self.devMode == True):
            print(message)
        elif (self.devMode == False):
            if(messageType == "Message"):
                arcpy.AddMessage(message)
            elif (messageType == "Warning"):
                arcpy.AddWarning(message)
    
    def addError(self, method, message, raiseError=True):
        """
        Adds error to output
    
        Args:
            method: Method in the class causing error
            message: Error Message
            raiseError: Raise error to to terminate the application
        """
        errMessage = "{0} - {1}".format(method, message)
    
        if(self.devMode == True):
            print errMessage
            traceback.print_exc()
            sys.exit()
        elif (self.devMode == False):
            arcpy.AddError(errMessage)
            if(raiseError):
                raise arcpy.ExecuteError
            