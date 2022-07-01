<?php

/**
 * This file is part of CodeIgniter 4 framework.
 *
 * (c) CodeIgniter Foundation <admin@codeigniter.com>
 *
 * For the full copyright and license information, please view
 * the LICENSE file that was distributed with this source code.
 */

// Validation language settings
return [
    // Core Messages
    'noRuleSets'      => 'No rulesets specified in Validation configuration.',
    'ruleNotFound'    => '{0} is not a valid rule.',
    'groupNotFound'   => '{0} is not a validation rules group.',
    'groupNotArray'   => '{0} rule group must be an array.',
    'invalidTemplate' => '{0} is not a valid Validation template.',

    // Rule Messages
    'alpha'                 => 'El campo {field} may only contain alphabetical characters.',
    'alpha_dash'            => 'El campo {field} may only contain alphanumeric, underscore, and dash characters.',
    'alpha_numeric'         => 'El campo {field} may only contain alphanumeric characters.',
    'alpha_numeric_punct'   => 'El campo {field} may contain only alphanumeric characters, spaces, and  ~ ! # $ % & * - _ + = | : . characters.',
    'alpha_numeric_space'   => 'El campo {field} may only contain alphanumeric and space characters.',
    'alpha_space'           => 'El campo {field} may only contain alphabetical characters and spaces.',
    'decimal'               => 'El campo {field} must contain a decimal number.',
    'differs'               => 'El campo {field} must differ from the {param} field.',
    'equals'                => 'El campo {field} must be exactly: {param}.',
    'exact_length'          => 'El campo {field} must be exactly {param} characters in length.',
    'greater_than'          => 'El campo {field} must contain a number greater than {param}.',
    'greater_than_equal_to' => 'El campo {field} must contain a number greater than or equal to {param}.',
    'hex'                   => 'El campo {field} may only contain hexidecimal characters.',
    'in_list'               => 'El campo {field} must be one of: {param}.',
    'integer'               => 'El campo {field} must contain an integer.',
    'is_natural'            => 'El campo {field} must only contain digits.',
    'is_natural_no_zero'    => 'El campo {field} must only contain digits and must be greater than zero.',
    'is_not_unique'         => 'El campo {field} must contain a previously existing value in the database.',
    'is_unique'             => 'El campo {field} must contain a unique value.',
    'less_than'             => 'El campo {field} must contain a number less than {param}.',
    'less_than_equal_to'    => 'El campo {field} must contain a number less than or equal to {param}.',
    'matches'               => 'El campo {field} does not match the {param} field.',
    'max_length'            => 'El campo {field} cannot exceed {param} characters in length.',
    'min_length'            => 'El campo {field} must be at least {param} characters in length.',
    'not_equals'            => 'El campo {field} cannot be: {param}.',
    'not_in_list'           => 'El campo {field} must not be one of: {param}.',
    'numeric'               => 'El campo {field} must contain only numbers.',
    'regex_match'           => 'El campo {field} is not in the correct format.',
    'required'              => 'El campo {field} es requerido.',
    'required_with'         => 'El campo {field} is required when {param} is present.',
    'required_without'      => 'El campo {field} is required when {param} is not present.',
    'string'                => 'El campo {field} must be a valid string.',
    'timezone'              => 'El campo {field} must be a valid timezone.',
    'valid_base64'          => 'El campo {field} must be a valid base64 string.',
    'valid_email'           => 'El campo {field} must contain a valid email address.',
    'valid_emails'          => 'El campo {field} must contain all valid email addresses.',
    'valid_ip'              => 'El campo {field} must contain a valid IP.',
    'valid_url'             => 'El campo {field} must contain a valid URL.',
    'valid_date'            => 'El campo {field} must contain a valid date.',

    // Credit Cards
    'valid_cc_num' => '{field} does not appear to be a valid credit card number.',

    // Files
    'uploaded' => '{field} is not a valid uploaded file.',
    'max_size' => '{field} is too large of a file.',
    'is_image' => '{field} is not a valid, uploaded image file.',
    'mime_in'  => '{field} does not have a valid mime type.',
    'ext_in'   => '{field} does not have a valid file extension.',
    'max_dims' => '{field} is either not an image, or it is too wide or tall.',
];
