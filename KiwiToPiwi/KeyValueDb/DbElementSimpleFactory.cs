#region License

// /*
// MIT License
// 
// Copyright (c) 2021 JackDalton2A
// XYZ
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// */

#endregion

using System;

namespace KiwiToPiwi.KeyValueDb
{
    static class DbElementSimpleFactory
    {
        internal static DbElement CreatFromByteArray(byte[] byteArray,AStringData aStringData, UStringData uStringData)
        {
            if (byteArray.Length < 2)
            {
                return null;
            }

            var dbElementId = (VTableIds) (byteArray[1] * 256 + byteArray[0]);
            DbElement result = null;
            switch (dbElementId)
            {
                case VTableIds.DB_UNKNOWN:
                    break;
                case VTableIds.DB_KEY_VECTOR:
                    result = new DbKeyVector(byteArray,aStringData,uStringData);
                    break;
                case VTableIds.DB_CASE:
                    break;
                case VTableIds.DB_CASES:
                    break;
                case VTableIds.DB_COMPU_BASE:
                    break;
                case VTableIds.DB_COMPU_INTERNAL_TO_PHYS:
                    break;
                case VTableIds.DB_COMPU_METHOD:
                    break;
                case VTableIds.DB_COMPU_PHYS_TO_INTERNAL:
                    break;
                case VTableIds.DB_COMPU_RATIONAL_COEFFS:
                    break;
                case VTableIds.DB_COMPU_SCALE:
                    break;
                case VTableIds.DB_COMPU_SCALES:
                    break;
                case VTableIds.DB_DEFAULT_CASE:
                    break;
                case VTableIds.DB_ECU_CONFIG_INFO:
                    break;
                case VTableIds.DB_DIAG_CODED_TYPE:
                    break;
                case VTableIds.DB_DOP_BASE:
                    break;
                case VTableIds.DB_DOP_DTC:
                    break;
                case VTableIds.DB_DOP_STRUCT:
                    break;
                case VTableIds.DB_DOP_SIMPLE_BASE:
                    break;
                case VTableIds.DB_ECU_VARIANT_PATTERN:
                    break;
                case VTableIds.DB_ECU_VARIANT_PATTERNS:
                    break;
                case VTableIds.DB_ENV_DATA:
                    break;
                case VTableIds.DB_ENV_DATA_REF_SET:
                    break;
                case VTableIds.DB_LAYER_DATA:
                    result = new DbLayerData(byteArray, aStringData, uStringData);
                    break;
                case VTableIds.DB_INTERNAL_CONSTRAINT:
                    break;
                case VTableIds.DB_PROJECT_DATA:
                    break;
                case VTableIds.DB_VEHICLE_INFO_DATA:
                    break;
                case VTableIds.DB_LIMIT:
                    break;
                case VTableIds.DB_MATCHING_PARAMETER:
                    break;
                case VTableIds.DB_MATCHING_PARAMETERS:
                    break;
                case VTableIds.DB_PHYSICAL_TYPE:
                    break;
                case VTableIds.MCD_DB_CODE_INFORMATION:
                    break;
                case VTableIds.MCD_DB_CODE_INFORMATIONS:
                    break;
                case VTableIds.DB_RELATED_SERVICES:
                    break;
                case VTableIds.DB_SCALE_CONSTRAINT:
                    break;
                case VTableIds.DB_SCALE_CONSTRAINTS:
                    break;
                case VTableIds.DB_SERVICE_PROTOCOL_PARAMETER:
                    break;
                case VTableIds.DB_SERVICE_PROTOCOL_PARAMETERS:
                    break;
                case VTableIds.DB_SWITCH_KEY:
                    break;
                case VTableIds.MCD_ACCESS_KEY:
                    break;
                case VTableIds.MCD_DB_ACCESS_LEVEL:
                    break;
                case VTableIds.MCD_DB_CONTROL_PRIMITIVES:
                    break;
                case VTableIds.MCD_DB_CONTROL_PRIMITIVE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_DATA_PRIMITIVES:
                    break;
                case VTableIds.MCD_DB_DATA_PRIMITIVE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_DIAG_COM_PRIMITIVES:
                    break;
                case VTableIds.MCD_DB_DIAG_COM_PRIMITIVE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_DIAG_SERVICES:
                    break;
                case VTableIds.MCD_DB_DIAG_SERVICE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_DIAG_TROUBLE_CODE:
                    break;
                case VTableIds.MCD_DB_DIAG_TROUBLE_CODES:
                    break;
                case VTableIds.MCD_DB_DIAG_TROUBLE_CODE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_BASE_VARIANT:
                    break;
                case VTableIds.MCD_DB_ECU_BASE_VARIANTS:
                    break;
                case VTableIds.MCD_DB_ECU_VARIANT:
                    break;
                case VTableIds.MCD_DB_ECU_VARIANTS:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_CLASS:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_CLASSES:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_CLASS_REFERENCES:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_GROUPS:
                    break;
                case VTableIds.MCD_DB_HELP_SERVICE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_INPUT_PARAM:
                    break;
                case VTableIds.MCD_DB_JOB:
                    break;
                case VTableIds.MCD_DB_JOB_REFERENCES:
                    break;
                case VTableIds.MCD_DB_JOBS:
                    break;
                case VTableIds.MCD_DB_LOCATION:
                    break;
                case VTableIds.MCD_DB_LOCATION_REFERENCES:
                    break;
                case VTableIds.MCD_DB_LOCATIONS:
                    break;
                case VTableIds.MCD_DB_LOGICAL_LINK:
                    break;
                case VTableIds.MCD_DB_LOGICAL_LINKS:
                    break;
                case VTableIds.MCD_DB_LOGICAL_LINK_REFERENCES:
                    break;
                case VTableIds.MCD_DB_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_VEHICLE_LINK_OR_INTERFACE:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_VEHICLE_LINK_OR_INTERFACES:
                    break;
                case VTableIds.MCD_XYZ:
                    break;
                case VTableIds.MCD_DB_PROJECT:
                    break;
                case VTableIds.MCD_DB_PROTOCOL_PARAMETER:
                    break;
                case VTableIds.MCD_DB_PROTOCOL_PARAMETER_SET:
                    break;
                case VTableIds.MCD_DB_REQUEST:
                    break;
                case VTableIds.MCD_DB_REQUEST_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_RESPONSE:
                    break;
                case VTableIds.MCD_DB_RESPONSE_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_PARAMETER_DYNAMIC_ENDMARKER_FIELD:
                    break;
                case VTableIds.MCD_DB_PARAMETER_DYNAMIC_LENGTH_FIELD:
                    break;
                case VTableIds.MCD_DB_PARAMETER_END_OF_PDU_FIELD:
                    break;
                case VTableIds.MCD_DB_PARAMETER_ENV_DATA_DESC:
                    break;
                case VTableIds.MCD_DB_PARAMETER_ENV_DATA:
                    break;
                case VTableIds.MCD_DB_PARAMETER_MULTIPLEXER:
                    break;
                case VTableIds.MCD_DB_PARAMETER_REFERENCES:
                    break;
                case VTableIds.MCD_DB_PARAMETER_SIMPLE:
                    break;
                case VTableIds.MCD_DB_PARAMETER_STATIC_FIELD:
                    break;
                case VTableIds.MCD_DB_MATCHING_REQUEST_PARAMETER:
                    break;
                case VTableIds.MCD_DB_PARAMETER_STRUCT_FIELD:
                    break;
                case VTableIds.MCD_DB_PARAMETER_STRUCTURE:
                    break;
                case VTableIds.MCD_DB_TABLE:
                    break;
                case VTableIds.MCD_DB_TABLE_PARAMETER:
                    break;
                case VTableIds.MCD_DB_TABLE_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_PARAMETER_TABLESTRUCT:
                    break;
                case VTableIds.MCD_DB_PARAMETER_TABLE_ENTRY:
                    break;
                case VTableIds.MCD_DB_PARAMETER_TABLE_KEY:
                    break;
                case VTableIds.MCD_DB_RESPONSES:
                    break;
                case VTableIds.MCD_DB_SERVICE:
                    break;
                case VTableIds.MCD_DB_SINGLE_ECU_JOB:
                    break;
                case VTableIds.MCD_DB_SERVICES:
                    break;
                case VTableIds.MCD_DB_SERVICE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_VEHICLE_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_VEHICLE_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_VEHICLE_CONNECTOR_PIN:
                    break;
                case VTableIds.MCD_DB_VEHICLE_CONNECTOR_PINS:
                    break;
                case VTableIds.MCD_DB_VEHICLE_CONNECTOR_PIN_REFERENCES:
                    break;
                case VTableIds.MCD_DB_VEHICLE_INFORMATION:
                    break;
                case VTableIds.MCD_DB_VEHICLE_INFORMATIONS:
                    break;
                case VTableIds.MCD_DB_ECU_VARIANT_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_BASE_VARIANT_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_MEM:
                    break;
                case VTableIds.MCD_DB_ECU_MEMS:
                    break;
                case VTableIds.MCD_DB_FLASH_CHECKSUM:
                    break;
                case VTableIds.MCD_DB_FLASH_CHECKSUMS:
                    break;
                case VTableIds.MCD_DB_FLASH_DATA_BLOCK:
                    break;
                case VTableIds.MCD_DB_FLASH_DATA_BLOCKS:
                    break;
                case VTableIds.MCD_DB_FLASH_DATA:
                    break;
                case VTableIds.MCD_DB_FLASH_FILTER:
                    break;
                case VTableIds.MCD_DB_FLASH_FILTERS:
                    break;
                case VTableIds.MCD_DB_FLASH_IDENT:
                    break;
                case VTableIds.MCD_DB_FLASH_IDENTS:
                    break;
                case VTableIds.MCD_DB_FLASH_SECURITY:
                    break;
                case VTableIds.MCD_DB_FLASH_SECURITIES:
                    break;
                case VTableIds.MCD_DB_FLASH_SEGMENT:
                    break;
                case VTableIds.MCD_DB_FLASH_SEGMENTS:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSION_CLASS:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSION_CLASSES:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSION:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSIONS:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_SEGMENT:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_SEGMENTS:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_MEMORY:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_MEMORIES:
                    break;
                case VTableIds.MCD_DB_FLASH_JOB:
                    break;
                case VTableIds.MCD_DB_IDENT_DESCRIPTION:
                    break;
                case VTableIds.MCD_VALUES:
                    break;
                case VTableIds.MCD_INTERVAL:
                    break;
                case VTableIds.MCD_ACCESS_KEYS:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_GROUP:
                    break;
                case VTableIds.MCD_TEXT_TABLE_ELEMENT:
                    break;
                case VTableIds.MCD_TEXT_TABLE_ELEMENTS:
                    break;
                case VTableIds.MCD_DB_DIAG_VARIABLE:
                    break;
                case VTableIds.MCD_DB_DIAG_VARIABLES:
                    break;
                case VTableIds.MCD_DB_UNIT:
                    break;
                case VTableIds.MCD_DB_UNITS:
                    break;
                case VTableIds.MCD_DB_UNIT_GROUP:
                    break;
                case VTableIds.MCD_DB_UNIT_GROUPS:
                    break;
                case VTableIds.MCD_DB_DATA_PRIMITIVE:
                    break;
                case VTableIds.MCD_DB_STARTCOMMUNICATION:
                    break;
                case VTableIds.MCD_DB_STOPCOMMUNICATION:
                    break;
                case VTableIds.MCD_DB_VARIANTIDENTIFICATION:
                    break;
                case VTableIds.MCD_DB_VARIANTIDENTIFICATIONANDSELECTION:
                    break;
                case VTableIds.MCD_DB_PROTOCOLPARAMETERSET:
                    break;
                case VTableIds.MCD_DB_PHYSICAL_DIMENSION:
                    break;
                case VTableIds.MCD_DB_ECU:
                    break;
                case VTableIds.MCD_DB_FUNCTIONAL_GROUP_REFERENCES:
                    break;
                case VTableIds.MCD_DB_SPECIAL_DATA_GROUPS:
                    break;
                case VTableIds.MCD_DB_SPECIAL_DATA_GROUP:
                    break;
                case VTableIds.MCD_DB_SPECIAL_DATA_ELEMENT:
                    break;
                case VTableIds.MCD_DB_DYN_ID_DEFINE_COM_PRIMITIVE:
                    break;
                case VTableIds.MCD_DB_DYN_ID_READ_COM_PRIMITIVE:
                    break;
                case VTableIds.MCD_DB_DYN_ID_CLEAR_COM_PRIMITIVE:
                    break;
                case VTableIds.MCD_AUDIENCE:
                    break;
                case VTableIds.MCD_DB_MULTIPLE_ECU_JOB:
                    break;
                case VTableIds.MCD_DB_TABLES:
                    break;
                case VTableIds.MCD_DB_TABLE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_MEM_REFERENCES:
                    break;
                case VTableIds.MCD_DB_UNIT_REFERENCES:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSION_CLASS_REFERENCES:
                    break;
                case VTableIds.MCD_DB_FLASH_SESSION_REFERENCES:
                    break;
                case VTableIds.MCD_DB_HEX_SERVICE:
                    break;
                case VTableIds.MCD_DB_TABLE_PARAMETER_REFERENCES:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_DATA:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_DATAS:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_DATA_REFERENCES:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_ID_ITEM:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_RECORD:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_RECORDS:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_RECORD_REFERENCES:
                    break;
                case VTableIds.MCD_DB_CODING_DATA:
                    break;
                case VTableIds.MCD_DB_CONFIGURATION_ITEM:
                    break;
                case VTableIds.MCD_DB_DATA_ID_ITEM:
                    break;
                case VTableIds.MCD_DB_DATA_RECORD:
                    break;
                case VTableIds.MCD_DB_DATA_RECORDS:
                    break;
                case VTableIds.MCD_DB_DATA_RECORD_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ITEM_VALUE:
                    break;
                case VTableIds.MCD_DB_ITEM_VALUES:
                    break;
                case VTableIds.MCD_DB_OPTION_ITEM:
                    break;
                case VTableIds.MCD_DB_OPTION_ITEMS:
                    break;
                case VTableIds.MCD_DB_SYSTEM_ITEM:
                    break;
                case VTableIds.MCD_DB_SYSTEM_ITEMS:
                    break;
                case VTableIds.DB_DIAG_COM_DATA_CONNECTOR:
                    break;
                case VTableIds.DB_DIAG_COM_DATA_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_MATCHING_PARAMETER:
                    break;
                case VTableIds.MCD_DB_MATCHING_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_SUB_COMPONENT:
                    break;
                case VTableIds.MCD_DB_SUB_COMPONENTS:
                    break;
                case VTableIds.MCD_DB_SUB_COMPONENT_REFERENCES:
                    break;
                case VTableIds.MCD_DB_MATCHING_PATTERN:
                    break;
                case VTableIds.MCD_DB_MATCHING_PATTERNS:
                    break;
                case VTableIds.MCD_DB_SUB_COMPONENT_PARAM_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_SUB_COMPONENT_PARAM_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_ECU_STATE:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_CHART:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_CHARTS:
                    break;
                case VTableIds.MCD_DB_ECU_STATES:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_TRANSITION:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_TRANSITIONS:
                    break;
                case VTableIds.MCD_DB_EXTERNAL_ACCESS_METHOD:
                    break;
                case VTableIds.MCD_DB_PRECONDITION_DEFINITION:
                    break;
                case VTableIds.MCD_DB_PRECONDITION_DEFINITIONS:
                    break;
                case VTableIds.MCD_DB_STATE_TRANSITION_ACTION:
                    break;
                case VTableIds.MCD_DB_STATE_TRANSITION_ACTIONS:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_CHART_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ECU_STATE_TRANSITION_REFERENCES:
                    break;
                case VTableIds.MCD_DB_STATE_TRANSITION_ACTION_REFERENCES:
                    break;
                case VTableIds.MCD_DB_PRE_CONDITION_DEFINITION_REFERENCES:
                    break;
                case VTableIds.MCD_INTERNAL_CONSTRAINT:
                    break;
                case VTableIds.MCD_SCALE_CONSTRAINTS:
                    break;
                case VTableIds.MCD_SCALE_CONSTRAINT:
                    break;
                case VTableIds.MCD_CONSTRAINT:
                    break;
                case VTableIds.MCD_DB_SPECIAL_DATA_GROUP_CAPTION:
                    break;
                case VTableIds.MCD_DB_SPECIAL_DATA_GROUP_REFERENCES:
                    break;
                case VTableIds.MCD_DB_ADDITIONAL_AUDIENCES:
                    break;
                case VTableIds.MCD_DB_ADDITIONAL_AUDIENCE:
                    break;
                case VTableIds.DB_ODX_LINK:
                    break;
                case VTableIds.DB_ODX_LINKS:
                    break;
                case VTableIds.MCD_DB_COMPONENT_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_COMPONENT_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_DIAG_OBJECT_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_DIAG_TROUBLE_CODE_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_DIAG_TROUBLE_CODE_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_ENV_DATA_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_ENV_DATA_CONNECTORS:
                    break;
                case VTableIds.MCD_DB_ENV_DATA_DESC:
                    break;
                case VTableIds.MCD_DB_FUNCTION_DIAG_COM_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_FUNCTION_DICTIONARY:
                    break;
                case VTableIds.MCD_DB_FUNCTION_DICTIONARIES:
                    break;
                case VTableIds.MCD_DB_FUNCTION_IN_PARAMETER:
                    break;
                case VTableIds.MCD_DB_FUNCTION_IN_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_FUNCTION_OUT_PARAMETER:
                    break;
                case VTableIds.MCD_DB_FUNCTION_OUT_PARAMETERS:
                    break;
                case VTableIds.MCD_DB_FUNCTION_NODE:
                    break;
                case VTableIds.MCD_DB_TABLE_ROW_CONNECTOR:
                    break;
                case VTableIds.MCD_DB_TABLE_ROW_CONNECTORS:
                    break;
                case VTableIds.DB_FUNCTION_DICTIONARY_DATA:
                    break;
                case VTableIds.DB_COM_PARAM_SPEC:
                    break;
                case VTableIds.DB_COM_PARAM_SUB_SET:
                    break;
                case VTableIds.DB_FLASH_DATA:
                    break;
                case VTableIds.DB_INLINE_FLASH_DATA:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;

        }
    }
}